namespace ParseDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Boardgame_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.Boardgames",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Rank = c.Int(nullable: false),
                        YearPublished = c.Int(nullable: false),
                        MinPlayers = c.Int(nullable: false),
                        MaxPlayers = c.Int(nullable: false),
                        SuggestedPlayers = c.Int(nullable: false),
                        MinAge = c.Int(nullable: false),
                        MinPlaytime = c.Int(nullable: false),
                        MaxPlaytime = c.Int(nullable: false),
                        UsersRated = c.Int(nullable: false),
                        OwnedNum = c.Int(nullable: false),
                        BayesAverage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Boardgame_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.Designers",
                c => new
                    {
                        DesignerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Boardgame_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.DesignerId)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        FamilyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Boardgame_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.FamilyId)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.Mechanics",
                c => new
                    {
                        MechanicId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Boardgame_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.MechanicId)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.BoardgameTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Boardgame_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.TypeId)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoardgameTypes", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Mechanics", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Families", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Designers", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Categories", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Artists", "Boardgame_GameId", "dbo.Boardgames");
            DropIndex("dbo.BoardgameTypes", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Mechanics", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Families", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Designers", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Categories", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Artists", new[] { "Boardgame_GameId" });
            DropTable("dbo.Publishers");
            DropTable("dbo.BoardgameTypes");
            DropTable("dbo.Mechanics");
            DropTable("dbo.Families");
            DropTable("dbo.Designers");
            DropTable("dbo.Categories");
            DropTable("dbo.Boardgames");
            DropTable("dbo.Artists");
        }
    }
}
