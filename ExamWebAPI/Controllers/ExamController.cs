using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
