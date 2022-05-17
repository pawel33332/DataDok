namespace DataDok.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uzytkownicies",
                c => new
                    {
                        Uzytkownik_id = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false),
                        Nazwisko = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Haslo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Uzytkownik_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Uzytkownicies");
        }
    }
}
