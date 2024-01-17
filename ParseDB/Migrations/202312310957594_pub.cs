namespace ParseDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pub : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artists", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Categories", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Designers", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Families", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.Mechanics", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.BoardgameTypes", "Boardgame_GameId", "dbo.Boardgames");
            DropIndex("dbo.Artists", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Categories", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Designers", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Families", new[] { "Boardgame_GameId" });
            DropIndex("dbo.Mechanics", new[] { "Boardgame_GameId" });
            DropIndex("dbo.BoardgameTypes", new[] { "Boardgame_GameId" });
            CreateTable(
                "dbo.BoardgameArtists",
                c => new
                    {
                        Boardgame_GameId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boardgame_GameId, t.Artist_ArtistId })
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId, cascadeDelete: true)
                .Index(t => t.Boardgame_GameId)
                .Index(t => t.Artist_ArtistId);
            
            CreateTable(
                "dbo.CategoryBoardgames",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Boardgame_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Boardgame_GameId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.DesignerBoardgames",
                c => new
                    {
                        Designer_DesignerId = c.Int(nullable: false),
                        Boardgame_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Designer_DesignerId, t.Boardgame_GameId })
                .ForeignKey("dbo.Designers", t => t.Designer_DesignerId, cascadeDelete: true)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .Index(t => t.Designer_DesignerId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.FamilyBoardgames",
                c => new
                    {
                        Family_FamilyId = c.Int(nullable: false),
                        Boardgame_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Family_FamilyId, t.Boardgame_GameId })
                .ForeignKey("dbo.Families", t => t.Family_FamilyId, cascadeDelete: true)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .Index(t => t.Family_FamilyId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.MechanicBoardgames",
                c => new
                    {
                        Mechanic_MechanicId = c.Int(nullable: false),
                        Boardgame_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Mechanic_MechanicId, t.Boardgame_GameId })
                .ForeignKey("dbo.Mechanics", t => t.Mechanic_MechanicId, cascadeDelete: true)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .Index(t => t.Mechanic_MechanicId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.PublisherBoardgames",
                c => new
                    {
                        Publisher_PublisherId = c.Int(nullable: false),
                        Boardgame_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Publisher_PublisherId, t.Boardgame_GameId })
                .ForeignKey("dbo.Publishers", t => t.Publisher_PublisherId, cascadeDelete: true)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .Index(t => t.Publisher_PublisherId)
                .Index(t => t.Boardgame_GameId);
            
            CreateTable(
                "dbo.BoardgameTypeBoardgames",
                c => new
                    {
                        BoardgameType_TypeId = c.Int(nullable: false),
                        Boardgame_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardgameType_TypeId, t.Boardgame_GameId })
                .ForeignKey("dbo.BoardgameTypes", t => t.BoardgameType_TypeId, cascadeDelete: true)
                .ForeignKey("dbo.Boardgames", t => t.Boardgame_GameId, cascadeDelete: true)
                .Index(t => t.BoardgameType_TypeId)
                .Index(t => t.Boardgame_GameId);
            
            DropColumn("dbo.Artists", "Boardgame_GameId");
            DropColumn("dbo.Categories", "Boardgame_GameId");
            DropColumn("dbo.Designers", "Boardgame_GameId");
            DropColumn("dbo.Families", "Boardgame_GameId");
            DropColumn("dbo.Mechanics", "Boardgame_GameId");
            DropColumn("dbo.BoardgameTypes", "Boardgame_GameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BoardgameTypes", "Boardgame_GameId", c => c.Int());
            AddColumn("dbo.Mechanics", "Boardgame_GameId", c => c.Int());
            AddColumn("dbo.Families", "Boardgame_GameId", c => c.Int());
            AddColumn("dbo.Designers", "Boardgame_GameId", c => c.Int());
            AddColumn("dbo.Categories", "Boardgame_GameId", c => c.Int());
            AddColumn("dbo.Artists", "Boardgame_GameId", c => c.Int());
            DropForeignKey("dbo.BoardgameTypeBoardgames", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.BoardgameTypeBoardgames", "BoardgameType_TypeId", "dbo.BoardgameTypes");
            DropForeignKey("dbo.PublisherBoardgames", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.PublisherBoardgames", "Publisher_PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.MechanicBoardgames", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.MechanicBoardgames", "Mechanic_MechanicId", "dbo.Mechanics");
            DropForeignKey("dbo.FamilyBoardgames", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.FamilyBoardgames", "Family_FamilyId", "dbo.Families");
            DropForeignKey("dbo.DesignerBoardgames", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.DesignerBoardgames", "Designer_DesignerId", "dbo.Designers");
            DropForeignKey("dbo.CategoryBoardgames", "Boardgame_GameId", "dbo.Boardgames");
            DropForeignKey("dbo.CategoryBoardgames", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BoardgameArtists", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.BoardgameArtists", "Boardgame_GameId", "dbo.Boardgames");
            DropIndex("dbo.BoardgameTypeBoardgames", new[] { "Boardgame_GameId" });
            DropIndex("dbo.BoardgameTypeBoardgames", new[] { "BoardgameType_TypeId" });
            DropIndex("dbo.PublisherBoardgames", new[] { "Boardgame_GameId" });
            DropIndex("dbo.PublisherBoardgames", new[] { "Publisher_PublisherId" });
            DropIndex("dbo.MechanicBoardgames", new[] { "Boardgame_GameId" });
            DropIndex("dbo.MechanicBoardgames", new[] { "Mechanic_MechanicId" });
            DropIndex("dbo.FamilyBoardgames", new[] { "Boardgame_GameId" });
            DropIndex("dbo.FamilyBoardgames", new[] { "Family_FamilyId" });
            DropIndex("dbo.DesignerBoardgames", new[] { "Boardgame_GameId" });
            DropIndex("dbo.DesignerBoardgames", new[] { "Designer_DesignerId" });
            DropIndex("dbo.CategoryBoardgames", new[] { "Boardgame_GameId" });
            DropIndex("dbo.CategoryBoardgames", new[] { "Category_CategoryId" });
            DropIndex("dbo.BoardgameArtists", new[] { "Artist_ArtistId" });
            DropIndex("dbo.BoardgameArtists", new[] { "Boardgame_GameId" });
            DropTable("dbo.BoardgameTypeBoardgames");
            DropTable("dbo.PublisherBoardgames");
            DropTable("dbo.MechanicBoardgames");
            DropTable("dbo.FamilyBoardgames");
            DropTable("dbo.DesignerBoardgames");
            DropTable("dbo.CategoryBoardgames");
            DropTable("dbo.BoardgameArtists");
            CreateIndex("dbo.BoardgameTypes", "Boardgame_GameId");
            CreateIndex("dbo.Mechanics", "Boardgame_GameId");
            CreateIndex("dbo.Families", "Boardgame_GameId");
            CreateIndex("dbo.Designers", "Boardgame_GameId");
            CreateIndex("dbo.Categories", "Boardgame_GameId");
            CreateIndex("dbo.Artists", "Boardgame_GameId");
            AddForeignKey("dbo.BoardgameTypes", "Boardgame_GameId", "dbo.Boardgames", "GameId");
            AddForeignKey("dbo.Mechanics", "Boardgame_GameId", "dbo.Boardgames", "GameId");
            AddForeignKey("dbo.Families", "Boardgame_GameId", "dbo.Boardgames", "GameId");
            AddForeignKey("dbo.Designers", "Boardgame_GameId", "dbo.Boardgames", "GameId");
            AddForeignKey("dbo.Categories", "Boardgame_GameId", "dbo.Boardgames", "GameId");
            AddForeignKey("dbo.Artists", "Boardgame_GameId", "dbo.Boardgames", "GameId");
        }
    }
}
