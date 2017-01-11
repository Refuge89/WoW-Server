using Framework.Contants.Character;
using Shaolinq;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Framework.Contants;
using Platform;

namespace Framework.Database
{
    public class DBManager : BaseModel<Models>
    {
        public void Boot()
        {
            if (File.Exists("database.sqlite"))
                return;

            // Recria a base inteira
            this.model.Create(DatabaseCreationOptions.DeleteExistingDatabase);

            using (var scope = new DataAccessScope())
            {
                // Inserindo Usuarios
                var User = this.model.Users.Create();
                    User.name       = "John Doe";
                    User.username   = "john";
                    User.email      = "john@doe.com";
                    User.password   = "doe";
                    User.created_at = DateTime.Now;

                var User2 = this.model.Users.Create();
                    User2.name       = "Dabal Doe";
                    User2.username   = "doe";
                    User2.email      = "dabal@doe.com";
                    User2.password   = "doe";
                    User2.created_at = DateTime.Now;

                var User3 = this.model.Users.Create();
                    User3.name = "John Doe";
                    User3.username = "ban";
                    User3.email = "john@doe.com";
                    User3.password = "doe";
                    User3.created_at = DateTime.Now;
                    User3.bannet_at = DateTime.Now;

                // Inserindo Realm
                var RealmPVP = this.model.Realms.Create();
                    RealmPVP.flag = RealmFlag.NewPlayers;
                    RealmPVP.timezone = RealmTimezone.AnyLocale;
                    RealmPVP.type = RealmType.PVP;
                    RealmPVP.name = "Firetree";
                    RealmPVP.ip = "127.0.0.1:1001";
                    RealmPVP.created_at = DateTime.Now;

                var RealmPVE = this.model.Realms.Create();
                    RealmPVE.flag = RealmFlag.NewPlayers;
                    RealmPVE.timezone = RealmTimezone.UnitedStates;
                    RealmPVE.type = RealmType.Normal;
                    RealmPVE.name = "Quel'Thalas";
                    RealmPVE.ip = "127.0.0.1:1001";
                    RealmPVE.created_at = DateTime.Now;

                // Inserindo Dados Primarios
                var CharInfoHuman = this.model.CharacterCreationInfo.Create();
                    CharInfoHuman.Race = RaceID.HUMAN;
                    CharInfoHuman.Cinematic = 81;
                    CharInfoHuman.MapID = 0;
                    CharInfoHuman.MapZone = 12;
                    CharInfoHuman.MapX = -8949.95f;
                    CharInfoHuman.MapY = -132.493f;
                    CharInfoHuman.MapZ = 83.5312f;
                    CharInfoHuman.MapRotation = 1.0f;
                    CharInfoHuman.FactionId = 1;
                    CharInfoHuman.ModelM = 49;
                    CharInfoHuman.ModelF = 50;
                    CharInfoHuman.TeamId = 7;
                    CharInfoHuman.TaxiMask = 2;

                var CharInfoOrc = this.model.CharacterCreationInfo.Create();
                    CharInfoOrc.Race = RaceID.ORC;
                    CharInfoOrc.Cinematic = 21;
                    CharInfoOrc.MapID = 1;
                    CharInfoOrc.MapZone = 14;
                    CharInfoOrc.MapX = -618.518f;
                    CharInfoOrc.MapY = -4251.67f;
                    CharInfoOrc.MapZ = 38.718f;
                    CharInfoOrc.MapRotation = 1.0f;
                    CharInfoOrc.FactionId = 2;
                    CharInfoOrc.ModelM = 51;
                    CharInfoOrc.ModelF = 52;
                    CharInfoOrc.TeamId = 1;
                    CharInfoOrc.TaxiMask = 4194304;

                var CharInfoDwarf = this.model.CharacterCreationInfo.Create();
                    CharInfoDwarf.Race = RaceID.DWARF;
                    CharInfoDwarf.Cinematic = 41;
                    CharInfoDwarf.MapID = 0;
                    CharInfoDwarf.MapZone = 1;
                    CharInfoDwarf.MapX = -6240.32f;
                    CharInfoDwarf.MapY = 331.033f;
                    CharInfoDwarf.MapZ = 382.758f;
                    CharInfoDwarf.MapRotation = 6.17716f;
                    CharInfoDwarf.FactionId = 3;
                    CharInfoDwarf.ModelM = 53;
                    CharInfoDwarf.ModelF = 54;
                    CharInfoDwarf.TeamId = 7;
                    CharInfoDwarf.TaxiMask = 32;

                var CharInfoNightElves = this.model.CharacterCreationInfo.Create();
                    CharInfoNightElves.Race = RaceID.NIGHTELF;
                    CharInfoNightElves.Cinematic = 61;
                    CharInfoNightElves.MapID = 1;
                    CharInfoNightElves.MapZone = 141;
                    CharInfoNightElves.MapX = 10311.3f;
                    CharInfoNightElves.MapY = 832.463f;
                    CharInfoNightElves.MapZ = 1326.41f;
                    CharInfoNightElves.MapRotation = 5.69632f;
                    CharInfoNightElves.FactionId = 4;
                    CharInfoNightElves.ModelM = 55;
                    CharInfoNightElves.ModelF = 56;
                    CharInfoNightElves.TeamId = 7;
                    CharInfoNightElves.TaxiMask = 100663296;

                var CharInfoUndead = this.model.CharacterCreationInfo.Create();
                    CharInfoUndead.Race = RaceID.UNDEAD;
                    CharInfoUndead.Cinematic = 2;
                    CharInfoUndead.MapID = 0;
                    CharInfoUndead.MapZone = 85;
                    CharInfoUndead.MapX = 1676.35f; //1676.71f;
                    CharInfoUndead.MapY = 1677.45f; //1678.31f;
                    CharInfoUndead.MapZ = 121.67f;
                    CharInfoUndead.MapRotation = 3.14f; // 2.70526f;
                    CharInfoUndead.FactionId = 5;
                    CharInfoUndead.ModelM = 57;
                    CharInfoUndead.ModelF = 58;
                    CharInfoUndead.TeamId = 1;
                    CharInfoUndead.TaxiMask = 1024;

                var CharInfoTauren = this.model.CharacterCreationInfo.Create();
                    CharInfoTauren.Race = RaceID.TAUREN;
                    CharInfoTauren.Cinematic = 141;
                    CharInfoTauren.MapID = 1;
                    CharInfoTauren.MapZone = 215;
                    CharInfoTauren.MapX = -2917.58f;
                    CharInfoTauren.MapY = -257.98f;
                    CharInfoTauren.MapZ = 52.9968f;
                    CharInfoTauren.MapRotation = 1.0f;
                    CharInfoTauren.FactionId = 6;
                    CharInfoTauren.ModelM = 59;
                    CharInfoTauren.ModelF = 60;
                    CharInfoTauren.TeamId = 1;
                    CharInfoTauren.TaxiMask = 2097152;

                var CharInfoGnome = this.model.CharacterCreationInfo.Create();
                    CharInfoGnome.Race = RaceID.GNOME;
                    CharInfoGnome.Cinematic = 101;
                    CharInfoGnome.MapID = 0;
                    CharInfoGnome.MapZone = 1;
                    CharInfoGnome.MapX = -6240.32f;
                    CharInfoGnome.MapY = 331.033f;
                    CharInfoGnome.MapZ = 382.758f;
                    CharInfoGnome.MapRotation = 1.0f;
                    CharInfoGnome.FactionId = 115;
                    CharInfoGnome.ModelM = 1563;
                    CharInfoGnome.ModelF = 1564;
                    CharInfoGnome.TeamId = 7;
                    CharInfoGnome.TaxiMask = 32;

                var CharInfoTroll = this.model.CharacterCreationInfo.Create();
                    CharInfoTroll.Race = RaceID.TROLL;
                    CharInfoTroll.Cinematic = 121;
                    CharInfoTroll.MapID = 1;
                    CharInfoTroll.MapZone = 14;
                    CharInfoTroll.MapX = -618.518f;
                    CharInfoTroll.MapY = -4251.67f;
                    CharInfoTroll.MapZ = 38.718f;
                    CharInfoTroll.MapRotation = 1.0f;
                    CharInfoTroll.FactionId = 116;
                    CharInfoTroll.ModelM = 1478;
                    CharInfoTroll.ModelF = 1479;
                    CharInfoTroll.TeamId = 7;
                    CharInfoTroll.TaxiMask = 32;

                scope.Complete();
            }
        }
    }
}
