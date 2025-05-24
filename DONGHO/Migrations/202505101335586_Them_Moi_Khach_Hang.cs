namespace MEGATECH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Them_Moi_Khach_Hang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 150),
                        NgaySinh = c.DateTime(nullable: false),
                        Image = c.String(),
                        GioiTinh = c.Boolean(nullable: false),
                        DiaChi = c.String(nullable: false, maxLength: 500),
                        SoDienThoai = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 500),
                        CCCD = c.String(nullable: false, maxLength: 500),
                        TenDangNhap = c.String(nullable: false, maxLength: 50),
                        MatKhau = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KhachHang");
        }
    }
}
