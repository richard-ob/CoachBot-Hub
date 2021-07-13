using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachBot.Domain.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchData = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    SourceAddress = table.Column<string>(nullable: true),
                    HomeGoals = table.Column<int>(nullable: true),
                    AwayGoals = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegionName = table.Column<string>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DiscordUserId = table.Column<decimal>(nullable: true),
                    SteamID = table.Column<decimal>(nullable: true),
                    DisableDMNotifications = table.Column<bool>(nullable: false),
                    PlayingSince = table.Column<DateTime>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    HubRole = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Base64EncodedImage = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetImages_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscordGuildId = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guilds_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPositions",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPositions", x => new { x.PlayerId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_PlayerPositions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRatings_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    RconPassword = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DeactivatedDate = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servers_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servers_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servers_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Acronym = table.Column<string>(nullable: true),
                    LogoImageId = table.Column<int>(nullable: true),
                    BrandColour = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organisations_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organisations_AssetImages_LogoImageId",
                        column: x => x.LogoImageId,
                        principalTable: "AssetImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organisations_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TeamCode = table.Column<string>(nullable: true),
                    KitEmote = table.Column<string>(nullable: true),
                    BadgeEmote = table.Column<string>(nullable: true),
                    BadgeImageId = table.Column<int>(nullable: true),
                    TeamType = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: true),
                    GuildId = table.Column<int>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    FoundedDate = table.Column<DateTime>(nullable: true),
                    Form = table.Column<string>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_AssetImages_BadgeImageId",
                        column: x => x.BadgeImageId,
                        principalTable: "AssetImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentSeries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TournamentLogoId = table.Column<int>(nullable: true),
                    OrganisationId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentSeries_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentSeries_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentSeries_AssetImages_TournamentLogoId",
                        column: x => x.TournamentLogoId,
                        principalTable: "AssetImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentSeries_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(nullable: false),
                    SubTeamName = table.Column<string>(nullable: true),
                    SubTeamCode = table.Column<string>(nullable: true),
                    DiscordChannelId = table.Column<decimal>(nullable: false),
                    DiscordChannelName = table.Column<string>(nullable: true),
                    Formation = table.Column<int>(nullable: false),
                    UseClassicLineup = table.Column<bool>(nullable: false),
                    ChannelType = table.Column<int>(nullable: false),
                    SearchIgnoreList = table.Column<string>(nullable: true),
                    DisableSearchNotifications = table.Column<bool>(nullable: false),
                    DuplicityProtection = table.Column<bool>(nullable: false),
                    Inactive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channels_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Channels_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TeamRole = table.Column<int>(nullable: false),
                    IsPending = table.Column<bool>(nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false),
                    LeaveDate = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    TournamentSeriesId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    TournamentType = table.Column<int>(nullable: false),
                    TeamType = table.Column<int>(nullable: false),
                    Format = table.Column<int>(nullable: false),
                    FantasyPointsLimit = table.Column<int>(nullable: false),
                    WinningTeamId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentSeries_TournamentSeriesId",
                        column: x => x.TournamentSeriesId,
                        principalTable: "TournamentSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournaments_Teams_WinningTeamId",
                        column: x => x.WinningTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChannelPositions",
                columns: table => new
                {
                    ChannelId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    Ordinal = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelPositions", x => new { x.ChannelId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_ChannelPositions_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelPositions_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChannelPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChannelId = table.Column<int>(nullable: true),
                    TeamType = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lineups_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Searches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChannelId = table.Column<int>(nullable: false),
                    DiscordSearchMessages = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Searches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Searches_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubstitutionRequests",
                columns: table => new
                {
                    Token = table.Column<string>(nullable: false),
                    ChannelId = table.Column<int>(nullable: false),
                    ServerId = table.Column<int>(nullable: false),
                    DiscordMessageId = table.Column<decimal>(nullable: false),
                    AcceptedById = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    AcceptedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstitutionRequests", x => x.Token);
                    table.ForeignKey(
                        name: "FK_SubstitutionRequests_Players_AcceptedById",
                        column: x => x.AcceptedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubstitutionRequests_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubstitutionRequests_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PositionGroup = table.Column<int>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    TournamentId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FantasyPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyPlayers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyPlayers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true),
                    TournamentId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamHomeId = table.Column<int>(nullable: true),
                    TeamAwayId = table.Column<int>(nullable: true),
                    ServerId = table.Column<int>(nullable: true),
                    MatchStatisticsId = table.Column<int>(nullable: true),
                    KickOff = table.Column<DateTime>(nullable: true),
                    Format = table.Column<int>(nullable: false),
                    MatchType = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_MatchStatistics_MatchStatisticsId",
                        column: x => x.MatchStatisticsId,
                        principalTable: "MatchStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamAwayId",
                        column: x => x.TeamAwayId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamHomeId",
                        column: x => x.TeamHomeId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentMatchDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TournamentId = table.Column<int>(nullable: false),
                    MatchDay = table.Column<int>(nullable: false),
                    MatchTime = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentMatchDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentMatchDays_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentMatchDays_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentMatchDays_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentStaff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentStaff_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentStaff_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentStaff_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentStaff_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentStages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TournamentId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentStages_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLineupPositions",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    LineupId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLineupPositions", x => new { x.PlayerId, x.PositionId, x.LineupId });
                    table.ForeignKey(
                        name: "FK_PlayerLineupPositions_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerLineupPositions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerLineupPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLineupSubstitutes",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    LineupId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLineupSubstitutes", x => new { x.PlayerId, x.LineupId });
                    table.ForeignKey(
                        name: "FK_PlayerLineupSubstitutes_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerLineupSubstitutes_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeamSelections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsFlex = table.Column<bool>(nullable: false),
                    FantasyPlayerId = table.Column<int>(nullable: true),
                    FantasyTeamId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeamSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FantasyTeamSelections_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyTeamSelections_FantasyPlayers_FantasyPlayerId",
                        column: x => x.FantasyPlayerId,
                        principalTable: "FantasyPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyTeamSelections_FantasyTeams_FantasyTeamId",
                        column: x => x.FantasyTeamId,
                        principalTable: "FantasyTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matchups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineupHomeId = table.Column<int>(nullable: true),
                    LineupAwayId = table.Column<int>(nullable: true),
                    MatchId = table.Column<int>(nullable: true),
                    ReadiedDate = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matchups_Lineups_LineupAwayId",
                        column: x => x.LineupAwayId,
                        principalTable: "Lineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchups_Lineups_LineupHomeId",
                        column: x => x.LineupHomeId,
                        principalTable: "Lineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchups_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMatchStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedCards = table.Column<int>(nullable: false),
                    YellowCards = table.Column<int>(nullable: false),
                    Fouls = table.Column<int>(nullable: false),
                    FoulsSuffered = table.Column<int>(nullable: false),
                    SlidingTackles = table.Column<int>(nullable: false),
                    SlidingTacklesCompleted = table.Column<int>(nullable: false),
                    GoalsConceded = table.Column<int>(nullable: false),
                    Shots = table.Column<int>(nullable: false),
                    ShotsOnGoal = table.Column<int>(nullable: false),
                    PassesCompleted = table.Column<int>(nullable: false),
                    Interceptions = table.Column<int>(nullable: false),
                    Offsides = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    OwnGoals = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    Passes = table.Column<int>(nullable: false),
                    FreeKicks = table.Column<int>(nullable: false),
                    Penalties = table.Column<int>(nullable: false),
                    Corners = table.Column<int>(nullable: false),
                    ThrowIns = table.Column<int>(nullable: false),
                    KeeperSaves = table.Column<int>(nullable: false),
                    GoalKicks = table.Column<int>(nullable: false),
                    Possession = table.Column<int>(nullable: false),
                    DistanceCovered = table.Column<int>(nullable: false),
                    KeeperSavesCaught = table.Column<int>(nullable: false),
                    PossessionPercentage = table.Column<int>(nullable: false),
                    MatchOutcome = table.Column<int>(nullable: false),
                    SecondsPlayed = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    Substitute = table.Column<bool>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    MatchTeamType = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatchStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStatistics_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPositionMatchStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedCards = table.Column<int>(nullable: false),
                    YellowCards = table.Column<int>(nullable: false),
                    Fouls = table.Column<int>(nullable: false),
                    FoulsSuffered = table.Column<int>(nullable: false),
                    SlidingTackles = table.Column<int>(nullable: false),
                    SlidingTacklesCompleted = table.Column<int>(nullable: false),
                    GoalsConceded = table.Column<int>(nullable: false),
                    Shots = table.Column<int>(nullable: false),
                    ShotsOnGoal = table.Column<int>(nullable: false),
                    PassesCompleted = table.Column<int>(nullable: false),
                    Interceptions = table.Column<int>(nullable: false),
                    Offsides = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    OwnGoals = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    Passes = table.Column<int>(nullable: false),
                    FreeKicks = table.Column<int>(nullable: false),
                    Penalties = table.Column<int>(nullable: false),
                    Corners = table.Column<int>(nullable: false),
                    ThrowIns = table.Column<int>(nullable: false),
                    KeeperSaves = table.Column<int>(nullable: false),
                    GoalKicks = table.Column<int>(nullable: false),
                    Possession = table.Column<int>(nullable: false),
                    DistanceCovered = table.Column<int>(nullable: false),
                    KeeperSavesCaught = table.Column<int>(nullable: false),
                    PossessionPercentage = table.Column<int>(nullable: false),
                    MatchOutcome = table.Column<int>(nullable: false),
                    SecondsPlayed = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    Substitute = table.Column<bool>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    MatchTeamType = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPositionMatchStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerPositionMatchStatistics_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPositionMatchStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPositionMatchStatistics_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerPositionMatchStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamMatchStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedCards = table.Column<int>(nullable: false),
                    YellowCards = table.Column<int>(nullable: false),
                    Fouls = table.Column<int>(nullable: false),
                    FoulsSuffered = table.Column<int>(nullable: false),
                    SlidingTackles = table.Column<int>(nullable: false),
                    SlidingTacklesCompleted = table.Column<int>(nullable: false),
                    GoalsConceded = table.Column<int>(nullable: false),
                    Shots = table.Column<int>(nullable: false),
                    ShotsOnGoal = table.Column<int>(nullable: false),
                    PassesCompleted = table.Column<int>(nullable: false),
                    Interceptions = table.Column<int>(nullable: false),
                    Offsides = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    OwnGoals = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    Passes = table.Column<int>(nullable: false),
                    FreeKicks = table.Column<int>(nullable: false),
                    Penalties = table.Column<int>(nullable: false),
                    Corners = table.Column<int>(nullable: false),
                    ThrowIns = table.Column<int>(nullable: false),
                    KeeperSaves = table.Column<int>(nullable: false),
                    GoalKicks = table.Column<int>(nullable: false),
                    Possession = table.Column<int>(nullable: false),
                    DistanceCovered = table.Column<int>(nullable: false),
                    KeeperSavesCaught = table.Column<int>(nullable: false),
                    PossessionPercentage = table.Column<int>(nullable: false),
                    MatchOutcome = table.Column<int>(nullable: false),
                    TeamName = table.Column<string>(nullable: true),
                    MatchId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    MatchTeamType = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMatchStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMatchStatistics_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMatchStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TournamentStageId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentGroups_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentGroups_TournamentStages_TournamentStageId",
                        column: x => x.TournamentStageId,
                        principalTable: "TournamentStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentGroups_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentPhases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TournamentStageId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentPhases_TournamentStages_TournamentStageId",
                        column: x => x.TournamentStageId,
                        principalTable: "TournamentStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGroupTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(nullable: false),
                    TournamentGroupId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGroupTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentGroupTeams_Players_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentGroupTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentGroupTeams_TournamentGroups_TournamentGroupId",
                        column: x => x.TournamentGroupId,
                        principalTable: "TournamentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentGroupTeams_Players_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FantasyPlayerPhases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Points = table.Column<int>(nullable: false),
                    PositionGroup = table.Column<int>(nullable: false),
                    FantasyPlayerId = table.Column<int>(nullable: false),
                    TournamentPhaseId = table.Column<int>(nullable: true),
                    PlayerMatchStatisticsId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyPlayerPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FantasyPlayerPhases_FantasyPlayers_FantasyPlayerId",
                        column: x => x.FantasyPlayerId,
                        principalTable: "FantasyPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyPlayerPhases_PlayerMatchStatistics_PlayerMatchStatisticsId",
                        column: x => x.PlayerMatchStatisticsId,
                        principalTable: "PlayerMatchStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyPlayerPhases_TournamentPhases_TournamentPhaseId",
                        column: x => x.TournamentPhaseId,
                        principalTable: "TournamentPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScorePredictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HomeGoalsPrediction = table.Column<int>(nullable: false),
                    AwayGoalsPrediction = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    TournamentPhaseId = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorePredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorePredictions_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScorePredictions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScorePredictions_TournamentPhases_TournamentPhaseId",
                        column: x => x.TournamentPhaseId,
                        principalTable: "TournamentPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGroupMatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: false),
                    TournamentGroupId = table.Column<int>(nullable: false),
                    TournamentPhaseId = table.Column<int>(nullable: false),
                    TeamHomePlaceholder = table.Column<string>(nullable: true),
                    TeamAwayPlaceholder = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGroupMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentGroupMatches_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentGroupMatches_TournamentGroups_TournamentGroupId",
                        column: x => x.TournamentGroupId,
                        principalTable: "TournamentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentGroupMatches_TournamentPhases_TournamentPhaseId",
                        column: x => x.TournamentPhaseId,
                        principalTable: "TournamentPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 117, "AF", "Afghanistan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 116, "PL", "Poland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 54, "PH", "Philippines" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 71, "PE", "Peru" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 73, "PY", "Paraguay" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 70, "PA", "Panama" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 115, "PK", "Pakistan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 14, "OM", "Oman" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 113, "NO", "Norway" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 108, "MK", "North Macedonia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 26, "NG", "Nigeria" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 69, "NI", "Nicaragua" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 53, "NZ", "New Zealand" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 90, "NL", "Netherlands" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 114, "NP", "Nepal" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 112, "MM", "Myanmar" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 13, "MA", "Morocco" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 129, "ME", "Montenegro" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 11, "LB", "Lebanon" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 12, "LY", "Libya" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 39, "LI", "Liechtenstein" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 106, "LT", "Lithuania" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 40, "LU", "Luxembourg" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 141, "MO", "Macao SAR" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 119, "PT", "Portugal" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 52, "MY", "Malaysia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 88, "ML", "Mali" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 111, "MT", "Malta" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 68, "MX", "Mexico" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 120, "MD", "Moldova" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 87, "MC", "Monaco" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 109, "MN", "Mongolia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 41, "MV", "Maldives" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 72, "PR", "Puerto Rico" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 15, "QA", "Qatar" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 89, "RE", "Réunion" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 133, "TH", "Thailand" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 56, "TT", "Trinidad and Tobago" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 18, "TN", "Tunisia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 137, "TR", "Turkey" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 135, "TM", "Turkmenistan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 138, "UA", "Ukraine" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 132, "TJ", "Tajikistan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 4, "AE", "United Arab Emirates" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 32, "US", "United States" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 75, "UY", "Uruguay" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 139, "UZ", "Uzbekistan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 76, "VE", "Venezuela" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 140, "VN", "Vietnam" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 104, "001", "World" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 34, "GB", "United Kingdom" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 107, "LV", "Latvia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 142, "TW", "Taiwan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 37, "CH", "Switzerland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 121, "RO", "Romania" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 23, "RU", "Russia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 122, "RW", "Rwanda" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 16, "SA", "Saudi Arabia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 79, "SN", "Senegal" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 130, "RS", "Serbia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 17, "SY", "Syria" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 55, "SG", "Singapore" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 126, "SI", "Slovenia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 127, "SO", "Somalia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2, "ZA", "South Africa" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 31, "ES", "Spain" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 124, "LK", "Sri Lanka" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 123, "SE", "Sweden" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 125, "SK", "Slovakia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 58, "419", "Latin America" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 105, "LA", "Laos" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 103, "KG", "Kyrgyzstan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 25, "BG", "Bulgaria" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 101, "KH", "Cambodia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 85, "CM", "Cameroon" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 47, "CA", "Canada" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 44, "029", "Caribbean" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 20, "CL", "Chile" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 110, "BN", "Brunei" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 28, "CN", "China" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 83, "CD", "Congo (DRC)" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 62, "CR", "Costa Rica" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 84, "CI", "Côte d’Ivoire" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 92, "HR", "Croatia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 63, "CU", "Cuba" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 33, "CZ", "Czechia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 61, "CO", "Colombia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 35, "DK", "Denmark" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 118, "BR", "Brazil" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 30, "BA", "Bosnia and Herzegovina" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 128, "AL", "Albania" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 6, "DZ", "Algeria" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 59, "AR", "Argentina" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 94, "AM", "Armenia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 45, "AU", "Australia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 36, "AT", "Austria" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 136, "BW", "Botswana" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 22, "AZ", "Azerbaijan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 27, "BD", "Bangladesh" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 24, "BY", "Belarus" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 82, "BE", "Belgium" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 46, "BZ", "Belize" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 42, "BT", "Bhutan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 60, "BO", "Bolivia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 5, "BH", "Bahrain" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 19, "YE", "Yemen" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1, "DJ", "Djibouti" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 65, "EC", "Ecuador" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 21, "IN", "India" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 49, "ID", "Indonesia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 78, "IR", "Iran" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 8, "IQ", "Iraq" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 50, "IE", "Ireland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 91, "IL", "Israel" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 95, "IS", "Iceland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 96, "IT", "Italy" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 97, "JP", "Japan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 9, "JO", "Jordan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 99, "KZ", "Kazakhstan" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 131, "KE", "Kenya" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 102, "KR", "Korea" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 10, "KW", "Kuwait" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 51, "JM", "Jamaica" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 64, "DO", "Dominican Republic" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 93, "HU", "Hungary" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 67, "HN", "Honduras" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 7, "EG", "Egypt" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 74, "SV", "El Salvador" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 134, "ER", "Eritrea" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 77, "EE", "Estonia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 3, "ET", "Ethiopia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 81, "FO", "Faroe Islands" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 48, "HK", "Hong Kong SAR" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 80, "FI", "Finland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 98, "GE", "Georgia" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 38, "DE", "Germany" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 43, "GR", "Greece" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 100, "GL", "Greenland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 66, "GT", "Guatemala" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 86, "HT", "Haiti" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 29, "FR", "France" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 57, "ZW", "Zimbabwe" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetImages_CreatedById",
                table: "AssetImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelPositions_CreatedById",
                table: "ChannelPositions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelPositions_PositionId",
                table: "ChannelPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_CreatedById",
                table: "Channels",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_DiscordChannelId",
                table: "Channels",
                column: "DiscordChannelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_TeamId",
                table: "Channels",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_UpdatedById",
                table: "Channels",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyPlayerPhases_FantasyPlayerId",
                table: "FantasyPlayerPhases",
                column: "FantasyPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyPlayerPhases_PlayerMatchStatisticsId",
                table: "FantasyPlayerPhases",
                column: "PlayerMatchStatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyPlayerPhases_TournamentPhaseId",
                table: "FantasyPlayerPhases",
                column: "TournamentPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyPlayers_PlayerId",
                table: "FantasyPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyPlayers_TeamId",
                table: "FantasyPlayers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyPlayers_TournamentId",
                table: "FantasyPlayers",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_CreatedById",
                table: "FantasyTeams",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_TournamentId",
                table: "FantasyTeams",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_UpdatedById",
                table: "FantasyTeams",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_PlayerId_TournamentId",
                table: "FantasyTeams",
                columns: new[] { "PlayerId", "TournamentId" },
                unique: true,
                filter: "[PlayerId] IS NOT NULL AND [TournamentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamSelections_CreatedById",
                table: "FantasyTeamSelections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamSelections_FantasyPlayerId",
                table: "FantasyTeamSelections",
                column: "FantasyPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamSelections_FantasyTeamId",
                table: "FantasyTeamSelections",
                column: "FantasyTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_CreatedById",
                table: "Guilds",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_DiscordGuildId",
                table: "Guilds",
                column: "DiscordGuildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_ChannelId",
                table: "Lineups",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CreatedById",
                table: "Matches",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchStatisticsId",
                table: "Matches",
                column: "MatchStatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ServerId",
                table: "Matches",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamAwayId",
                table: "Matches",
                column: "TeamAwayId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamHomeId",
                table: "Matches",
                column: "TeamHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UpdatedById",
                table: "Matches",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Matchups_LineupAwayId",
                table: "Matchups",
                column: "LineupAwayId",
                unique: true,
                filter: "[LineupAwayId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Matchups_LineupHomeId",
                table: "Matchups",
                column: "LineupHomeId",
                unique: true,
                filter: "[LineupHomeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Matchups_MatchId",
                table: "Matchups",
                column: "MatchId",
                unique: true,
                filter: "[MatchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_CreatedById",
                table: "Organisations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_LogoImageId",
                table: "Organisations",
                column: "LogoImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_UpdatedById",
                table: "Organisations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLineupPositions_LineupId",
                table: "PlayerLineupPositions",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLineupPositions_PositionId_LineupId",
                table: "PlayerLineupPositions",
                columns: new[] { "PositionId", "LineupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLineupSubstitutes_LineupId",
                table: "PlayerLineupSubstitutes",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStatistics_MatchId",
                table: "PlayerMatchStatistics",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStatistics_PlayerId",
                table: "PlayerMatchStatistics",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStatistics_TeamId",
                table: "PlayerMatchStatistics",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositionMatchStatistics_MatchId",
                table: "PlayerPositionMatchStatistics",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositionMatchStatistics_PlayerId",
                table: "PlayerPositionMatchStatistics",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositionMatchStatistics_PositionId",
                table: "PlayerPositionMatchStatistics",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositionMatchStatistics_TeamId",
                table: "PlayerPositionMatchStatistics",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositions_PositionId",
                table: "PlayerPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRatings_PlayerId",
                table: "PlayerRatings",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CountryId",
                table: "Players",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CreatedById",
                table: "Players",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DiscordUserId",
                table: "Players",
                column: "DiscordUserId",
                unique: true,
                filter: "[DiscordUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SteamID",
                table: "Players",
                column: "SteamID",
                unique: true,
                filter: "[SteamID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UpdatedById",
                table: "Players",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_CreatedById",
                table: "PlayerTeams",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_PlayerId",
                table: "PlayerTeams",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_TeamId",
                table: "PlayerTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_UpdatedById",
                table: "PlayerTeams",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name",
                table: "Positions",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionName",
                table: "Regions",
                column: "RegionName",
                unique: true,
                filter: "[RegionName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScorePredictions_MatchId",
                table: "ScorePredictions",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorePredictions_PlayerId",
                table: "ScorePredictions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorePredictions_TournamentPhaseId",
                table: "ScorePredictions",
                column: "TournamentPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_ChannelId",
                table: "Searches",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_Address",
                table: "Servers",
                column: "Address",
                unique: true,
                filter: "[Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_CountryId",
                table: "Servers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_CreatedById",
                table: "Servers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_RegionId",
                table: "Servers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_UpdatedById",
                table: "Servers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_AcceptedById",
                table: "SubstitutionRequests",
                column: "AcceptedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_ChannelId",
                table: "SubstitutionRequests",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_ServerId",
                table: "SubstitutionRequests",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMatchStatistics_MatchId",
                table: "TeamMatchStatistics",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMatchStatistics_TeamId",
                table: "TeamMatchStatistics",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_BadgeImageId",
                table: "Teams",
                column: "BadgeImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatedById",
                table: "Teams",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GuildId",
                table: "Teams",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_RegionId",
                table: "Teams",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UpdatedById",
                table: "Teams",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamCode_RegionId",
                table: "Teams",
                columns: new[] { "TeamCode", "RegionId" },
                unique: true,
                filter: "[TeamCode] IS NOT NULL AND [RegionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupMatches_MatchId",
                table: "TournamentGroupMatches",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupMatches_TournamentGroupId",
                table: "TournamentGroupMatches",
                column: "TournamentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupMatches_TournamentPhaseId",
                table: "TournamentGroupMatches",
                column: "TournamentPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroups_CreatedById",
                table: "TournamentGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroups_TournamentStageId",
                table: "TournamentGroups",
                column: "TournamentStageId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroups_UpdatedById",
                table: "TournamentGroups",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupTeams_CreatedById",
                table: "TournamentGroupTeams",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupTeams_TeamId",
                table: "TournamentGroupTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupTeams_TournamentGroupId",
                table: "TournamentGroupTeams",
                column: "TournamentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGroupTeams_UpdatedById",
                table: "TournamentGroupTeams",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchDays_CreatedById",
                table: "TournamentMatchDays",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchDays_TournamentId",
                table: "TournamentMatchDays",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchDays_UpdatedById",
                table: "TournamentMatchDays",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPhases_TournamentStageId",
                table: "TournamentPhases",
                column: "TournamentStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CreatedById",
                table: "Tournaments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentSeriesId",
                table: "Tournaments",
                column: "TournamentSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_UpdatedById",
                table: "Tournaments",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_WinningTeamId",
                table: "Tournaments",
                column: "WinningTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentSeries_CreatedById",
                table: "TournamentSeries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentSeries_OrganisationId",
                table: "TournamentSeries",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentSeries_TournamentLogoId",
                table: "TournamentSeries",
                column: "TournamentLogoId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentSeries_UpdatedById",
                table: "TournamentSeries",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentStaff_CreatedById",
                table: "TournamentStaff",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentStaff_TournamentId",
                table: "TournamentStaff",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentStaff_UpdatedById",
                table: "TournamentStaff",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentStaff_PlayerId_TournamentId",
                table: "TournamentStaff",
                columns: new[] { "PlayerId", "TournamentId" },
                unique: true,
                filter: "[TournamentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentStages_TournamentId",
                table: "TournamentStages",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelPositions");

            migrationBuilder.DropTable(
                name: "FantasyPlayerPhases");

            migrationBuilder.DropTable(
                name: "FantasyTeamSelections");

            migrationBuilder.DropTable(
                name: "Matchups");

            migrationBuilder.DropTable(
                name: "PlayerLineupPositions");

            migrationBuilder.DropTable(
                name: "PlayerLineupSubstitutes");

            migrationBuilder.DropTable(
                name: "PlayerPositionMatchStatistics");

            migrationBuilder.DropTable(
                name: "PlayerPositions");

            migrationBuilder.DropTable(
                name: "PlayerRatings");

            migrationBuilder.DropTable(
                name: "PlayerTeams");

            migrationBuilder.DropTable(
                name: "ScorePredictions");

            migrationBuilder.DropTable(
                name: "Searches");

            migrationBuilder.DropTable(
                name: "SubstitutionRequests");

            migrationBuilder.DropTable(
                name: "TeamMatchStatistics");

            migrationBuilder.DropTable(
                name: "TournamentGroupMatches");

            migrationBuilder.DropTable(
                name: "TournamentGroupTeams");

            migrationBuilder.DropTable(
                name: "TournamentMatchDays");

            migrationBuilder.DropTable(
                name: "TournamentStaff");

            migrationBuilder.DropTable(
                name: "PlayerMatchStatistics");

            migrationBuilder.DropTable(
                name: "FantasyPlayers");

            migrationBuilder.DropTable(
                name: "FantasyTeams");

            migrationBuilder.DropTable(
                name: "Lineups");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "TournamentPhases");

            migrationBuilder.DropTable(
                name: "TournamentGroups");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "TournamentStages");

            migrationBuilder.DropTable(
                name: "MatchStatistics");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentSeries");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "AssetImages");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
