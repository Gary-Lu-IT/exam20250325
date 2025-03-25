﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExamWebAPI.Models.DB;

public partial class testContext : DbContext
{
    public testContext()
    {
    }

    public testContext(DbContextOptions<testContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MyOffice_ACPD> MyOffice_ACPD { get; set; }

    public virtual DbSet<MyOffice_ExcuteionLog> MyOffice_ExcuteionLog { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("TrustServerCertificate=true;Data Source=localhost; Initial Catalog=test;User ID=gslog;Password=gslog;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyOffice_ACPD>(entity =>
        {
            entity.HasKey(e => e.ACPD_SID);

            entity.Property(e => e.ACPD_SID)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ACPD_Cname).HasMaxLength(60);
            entity.Property(e => e.ACPD_Email).HasMaxLength(60);
            entity.Property(e => e.ACPD_Ename).HasMaxLength(40);
            entity.Property(e => e.ACPD_LoginID).HasMaxLength(30);
            entity.Property(e => e.ACPD_LoginPWD).HasMaxLength(60);
            entity.Property(e => e.ACPD_Memo).HasMaxLength(600);
            entity.Property(e => e.ACPD_NowDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ACPD_NowID).HasMaxLength(20);
            entity.Property(e => e.ACPD_Sname).HasMaxLength(40);
            entity.Property(e => e.ACPD_Status).HasDefaultValue((byte)0);
            entity.Property(e => e.ACPD_Stop).HasDefaultValue(false);
            entity.Property(e => e.ACPD_StopMemo).HasMaxLength(60);
            entity.Property(e => e.ACPD_UPDDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ACPD_UPDID).HasMaxLength(20);
        });

        modelBuilder.Entity<MyOffice_ExcuteionLog>(entity =>
        {
            entity.HasKey(e => e.DeLog_AutoID).HasName("PK_MOTC_DataExchangeLog");

            entity.Property(e => e.DeLog_ExDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeLog_ExecutionProgram).HasMaxLength(120);
            entity.Property(e => e.DeLog_StoredPrograms).HasMaxLength(120);
            entity.Property(e => e.DeLog_verifyNeeded).HasDefaultValue(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
