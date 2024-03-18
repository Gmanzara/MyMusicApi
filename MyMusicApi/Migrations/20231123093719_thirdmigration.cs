using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMusicApi.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Artists(Name) values('Linkin Park')");
            migrationBuilder
                .Sql("INSERT INTO Artists(Name) values ('Iron Maiden')");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name) values('Flogging Molly')");
            migrationBuilder
                .Sql("INSERT INTO Artists(Name) values('Red Hot Chilli Peppers')");

            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('In the End', (SELECT Id FROM Artists WHERE Name ='Linkin Park')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('Numb', (SELECT Id FROM Artists WHERE Name ='Linkin Park')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('Breaking the habit', (SELECT Id FROM Artists WHERE Name ='Linkin Park')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('Fear of dark', (SELECT Id FROM Artists WHERE Name ='Iron Maiden')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('Number of the beast', (SELECT Id FROM Artists WHERE Name ='Iron Maiden')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('The tropper', (SELECT Id FROM Artists WHERE Name ='Flogging Molly')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('What''s left of the flag', (SELECT Id FROM Artists WHERE Name ='Flogging Molly')");
            migrationBuilder
               .Sql("INSERT INTO Musics (Name,ArtistId) values('Drunken Lullabies', (SELECT Id FROM Artists WHERE Name ='Flogging Molly')");
            migrationBuilder
               .Sql("INSERT INTO Musics (Name,ArtistId) values('If I ever leave this world Alive', (SELECT Id FROM Artists WHERE Name ='Flogging Molly')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('Californication', (SELECT Id FROM Artists WHERE Name ='Red Hot Chilli Peppers')");
            migrationBuilder
              .Sql("INSERT INTO Musics (Name,ArtistId) values('Tell Me Baby', (SELECT Id FROM Artists WHERE Name ='Red Hot Chilli Peppers')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name,ArtistId) values('Parallel Universe', (SELECT Id FROM Artists WHERE Name ='Red Hot Chilli Peppers')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELECTE FROM Musics");
            migrationBuilder
                .Sql("DELETE FROM Artists");
        }
    }
}
