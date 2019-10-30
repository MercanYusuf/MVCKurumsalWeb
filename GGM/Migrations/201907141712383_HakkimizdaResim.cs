namespace GGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HakkimizdaResim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HakkımızdaResim",
                c => new
                    {
                        HakkimizdaResimId = c.Int(nullable: false, identity: true),
                        ResimURL = c.String(),
                    })
                .PrimaryKey(t => t.HakkimizdaResimId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HakkımızdaResim");
        }
    }
}
