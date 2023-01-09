using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAPI.Migrations
{
    public partial class SeedingDataDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DECLARE @UserID as INT
                --------------------------
                --Create User
                --------------------------
                IF not exists (select Id from Users where Username='muhammad.khoirudin')
                insert into Users(Username,Password, PasswordKey,LastUpdatedOn,LastUpdatedBy)
                select 'muhammad.khoirudin',
                0xD336D4903C2DD329113BBC2398347620121186B95B88C1DD8589188E867E07B4C10F302AED24AC15BE8837FB319F22407EDA7E41B937CAC7A4B2F5938EF57121,
                0xE96E7CC7B77166DDAF00B7E7E9BD40E1FD32FAF9F1FA42021369F377804913DEF97AE220A5C540BFAA9BC573D7970513F8D0F99DF931B4ACECD42996E835CC04C7DDAE41A202DDF6170D847A21BC9F7B6567ED52272CEF664008BCEECA16595F801F189C1EF326AC7D55CE948D47A3958081B73BF1471FCBFD11B0FB38AE6611,
                getdate(),
                0
                SET @UserID = (select id from Users where Username='muhammad.khoirudin')
                --------------------------
                --Seed Property Types
                --------------------------
                IF not exists (select name from PropertyTypes where Name='House')
                insert into PropertyTypes(Name,LastUpdatedOn,LastUpdatedBy)
                select 'House', GETDATE(),@UserID
                IF not exists (select name from PropertyTypes where Name='Apartment')
                insert into PropertyTypes(Name,LastUpdatedOn,LastUpdatedBy)
                select 'Apartment', GETDATE(),@UserID
                    
                IF not exists (select name from PropertyTypes where Name='Hotel')
                insert into PropertyTypes(Name,LastUpdatedOn,LastUpdatedBy)
                select 'Hotel', GETDATE(),@UserID
                --------------------------
                --Seed Furniture Types
                --------------------------
                IF not exists (select name from FurnitureTypes where Name='Fully')
                insert into FurnitureTypes(Name, LastUpdatedOn, LastUpdatedBy)
                select 'Fully', GETDATE(),@UserID
                    
                IF not exists (select name from FurnitureTypes where Name='Semi')
                insert into FurnitureTypes(Name, LastUpdatedOn, LastUpdatedBy)
                select 'Semi', GETDATE(),@UserID
                    
                IF not exists (select name from FurnitureTypes where Name='Unfurnished')
                insert into FurnitureTypes(Name, LastUpdatedOn, LastUpdatedBy)
                select 'Unfurnished', GETDATE(),@UserID
                --------------------------
                --Seed Cities
                --------------------------
                IF not exists (select top 1 id from Cities)
                Insert into Cities(Name,LastUpdatedBy,LastUpdatedOn,Country)
                select 'Yogyakarta',@UserID,getdate(),'Indonesia'
                union
                select 'Jakarta',@UserID,getdate(),'Indonesia'
                union
                select 'Bali',@UserID,getdate(),'Indonesia'
                union
                select 'Queenstown',@UserID,getdate(),'Singapore'
                union
                select 'Kuala Lumpur',@UserID,getdate(),'Malaysia'
                --------------------------
                --Seed Properties
                --------------------------
                --Seed property for sell
                IF not exists (select top 1 name from BuildingProperties where Name='White House Demo')
                insert into BuildingProperties(SellRent,Name,PropertyTypeId,Bhk,FurnitureTypeId,Price,BuiltArea,CarpetArea,Address,
                Address2,CityId,FloorNo,TotalFloor,IsReadyToMove,MainEntrance,Security,Gated,Maintenance,PossessionOn,Age,Description,PostedOn,PostedBy,LastUpdatedOn,LastUpdatedBy)
                select 
                1, --Sell Rent
                'White House Demo', --Name
                (select Id from PropertyTypes where Name='Apartment'), --Property Type ID
                2, --BHK
                (select Id from FurnitureTypes where Name='Fully'), --Furniture Type ID
                1800, --Price
                1400, --Built Area
                900, --Carpet Area
                '6 Street', --Address
                'Golf Course Road', -- Address2
                (select top 1 Id from Cities), -- City ID
                3, -- Floor No
                3, --Total Floors
                1, --Ready to Move
                'East', --Main Entrance
                0, --Security
                1, --Gated
                300, -- Maintenance
                '2019-01-01', -- Establishment or Posession on
                0, --Age
                'Well Maintained builder floor available for rent at prime location. # property features- - 5 mins away from metro station - Gated community - 24*7 security. # property includes- - Big rooms (Cross ventilation & proper sunlight) - 
                Drawing and dining area - Washrooms - Balcony - Modular kitchen - Near gym, market, temple and park - Easy commuting to major destination. Feel free to call With Query.', --Description
                GETDATE(), --Posted on
                @UserID, --Posted by
                GETDATE(), --Last Updated on
                @UserID --Last Updated by
                ---------------------------
                --Seed property for rent
                ---------------------------
                IF not exists (select top 1 name from BuildingProperties where Name='Birla House Demo')
                insert into BuildingProperties(SellRent,Name,PropertyTypeId,Bhk,FurnitureTypeId,Price,BuiltArea,CarpetArea,Address,
                Address2,CityId,FloorNo,TotalFloor,IsReadyToMove,MainEntrance,Security,Gated,Maintenance,PossessionOn,Age,Description,PostedOn,PostedBy,LastUpdatedOn,LastUpdatedBy)
                select 
                2, --Sell Rent
                'Birla House Demo', --Name
                (select Id from PropertyTypes where Name='Apartment'), --Property Type ID
                2, --BHK
                (select Id from FurnitureTypes where Name='Fully'), --Furniture Type ID
                1800, --Price
                1400, --Built Area
                900, --Carpet Area
                '6 Street', --Address
                'Golf Course Road', -- Address2
                (select top 1 Id from Cities), -- City ID
                3, -- Floor No
                3, --Total Floors
                1, --Ready to Move
                'East', --Main Entrance
                0, --Security
                1, --Gated
                300, -- Maintenance
                '2019-01-01', -- Establishment or Posession on
                0, --Age
                'Well Maintained builder floor available for rent at prime location. # property features- - 5 mins away from metro station - Gated community - 24*7 security. # property includes- - Big rooms (Cross ventilation & proper sunlight) - 
                Drawing and dining area - Washrooms - Balcony - Modular kitchen - Near gym, market, temple and park - Easy commuting to major destination. Feel free to call With Query.', --Description
                GETDATE(), --Posted on
                @UserID, --Posted by
                GETDATE(), --Last Updated on
                @UserID --Last Updated by
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                --------------------
                --Seeding Down
                --------------------
                DECLARE @UserID as int
                SET @UserID = (select id from Users where Username='muhammad.khoirudin')
                delete from Users where Username='muhammad.khoirudin'
                delete from PropertyTypes where LastUpdatedBy=@UserID
                delete from FurnitureTypes where LastUpdatedBy=@UserID
                delete from Cities where LastUpdatedBy=@UserID
                delete from BuildingProperties where PostedBy=@UserId            
            ");
        }
    }
}
