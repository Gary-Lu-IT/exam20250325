using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ExamWebAPI.Controllers
{
    public class ExamController : Controller
    {
        /// <summary>
        /// 判斷輸入的數字是否為質數 (測試API是否正常運作用的，不連接資料庫)
        /// </summary>
        /// <param name="InputNumber"></param>
        /// <returns></returns>
        [HttpPost][AllowAnonymous][Route("api/exam/isprime")]
        public IActionResult IsPrime(int InputNumber)
        {
            if(InputNumber < 2)
            {
                return Ok(InputNumber.ToString()+"不是質數也不是合成數");
            }
            else
            {
                for (int i = 2; i < InputNumber; i++)
                {
                    if (InputNumber % i == 0)
                    {
                        return Ok(InputNumber.ToString() + "是合成數，找到2以上小於自身的因數："+i.ToString());
                    }
                }
                return Ok(InputNumber.ToString() + "是質數");
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ACPD"></param>
        /// <returns></returns>
        [HttpPost][AllowAnonymous][Route("api/exam/addcspd")]public IActionResult AddCSPD([FromBody] MyOffice_ACPD ACPD)
        {
            try
            {
                using (var db = new testContext())
                {
                    SqlParameter tblName = new SqlParameter
                    {
                        ParameterName = "@TName",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value="MyOffice_ACPD"
                    };
                    SqlParameter Sid = new SqlParameter
                    {
                        ParameterName = "@Sid",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Output,
                        Size=24
                    };
                    db.Database.ExecuteSqlRaw("exec NEWSID @TName,@Sid out", tblName, Sid);
                    ACPD.ACPD_SID = Sid.Value.ToString();
                    db.MyOffice_ACPD.Add(ACPD);
                    db.SaveChanges();
                }
                return Ok("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>變更</summary>
        /// <param name="ACPD"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/exam/updcspd")]
        public IActionResult UpdCSPD([FromBody] MyOffice_ACPD ACPD)
        {
            try
            {
                using (var db = new testContext())
                {
                    MyOffice_ACPD OldACPD = db.MyOffice_ACPD.Where(x => x.ACPD_SID == ACPD.ACPD_SID).FirstOrDefault();
                    OldACPD.ACPD_Cname = ACPD.ACPD_Cname;
                    OldACPD.ACPD_Sname = ACPD.ACPD_Sname;
                    OldACPD.ACPD_Ename = ACPD.ACPD_Ename;
                    OldACPD.ACPD_Email = ACPD.ACPD_Email;
                    OldACPD.ACPD_LoginID = ACPD.ACPD_LoginID;
                    OldACPD.ACPD_Memo = ACPD.ACPD_Memo;
                    OldACPD.ACPD_Status = ACPD.ACPD_Status;
                    OldACPD.ACPD_Stop = ACPD.ACPD_Stop;
                    OldACPD.ACPD_StopMemo = ACPD.ACPD_StopMemo;
                    OldACPD.ACPD_UPDDateTime = ACPD.ACPD_UPDDateTime;
                    OldACPD.ACPD_UPDID = ACPD.ACPD_UPDID;
                    db.SaveChanges();
                }
                return Ok("修改成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
