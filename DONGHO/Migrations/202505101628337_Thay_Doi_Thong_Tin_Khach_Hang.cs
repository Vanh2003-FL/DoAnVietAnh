namespace MEGATECH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thay_Doi_Thong_Tin_Khach_Hang : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KhachHang", "NgaySinh");
            DropColumn("dbo.KhachHang", "Image");
            DropColumn("dbo.KhachHang", "GioiTinh");
            DropColumn("dbo.KhachHang", "TenDangNhap");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KhachHang", "TenDangNhap", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.KhachHang", "GioiTinh", c => c.Boolean(nullable: false));
            AddColumn("dbo.KhachHang", "Image", c => c.String());
            AddColumn("dbo.KhachHang", "NgaySinh", c => c.DateTime(nullable: false));
        }
    }
}
