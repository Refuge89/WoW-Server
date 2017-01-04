using Framework.Database;
using Framework.Database.Tables;
using Framework.Database.Xml;
using Shaolinq;

namespace World_Server.Helpers
{
    class CharHelper : BaseModel<Models>
    {
        internal void GeraSpells(Character character)
        {
            #region Select spell of race
            foreach (raceSpell spellid in XmlManager.GetRaceStats(character.Race).spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var Spell = this.model.CharactersSpells.Create();
                        Spell.character = character;
                        Spell.spell = spellid.id;
                        Spell.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }
            #endregion

            #region Select spell of class
            foreach (classeSpell spellid in XmlManager.GetClassStats(character.Class).spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var Spell = this.model.CharactersSpells.Create();
                        Spell.character = character;
                        Spell.spell = spellid.id;
                        Spell.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }
            #endregion

            #region Select spell combo (race + class)
            foreach (raceClass spellid in XmlManager.GetRaceStats(character.Race).classes)
            {
                if(spellid.id == character.Class.ToString())
                {
                    foreach (raceClassSpell spell2Id in spellid.spells)
                    {
                        using (var scope = new DataAccessScope())
                        {
                            var Spell = this.model.CharactersSpells.Create();
                                Spell.character = character;
                                Spell.spell = spell2Id.id;
                                Spell.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }
            #endregion

            return;                    
        }

        internal void GeraSkills(Character character)
        {
            // Select skill of race
            foreach (raceSkill skillId in XmlManager.GetRaceStats(character.Race).skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var Skill = this.model.CharactersSkill.Create();
                        Skill.character = character;
                        Skill.skill     = skillId.id;
                        Skill.Max       = skillId.max;
                        Skill.value = skillId.min;
                        Skill.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            // Select skill of class
            foreach (classeSkill skillId in XmlManager.GetClassStats(character.Class).skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var Skill = this.model.CharactersSkill.Create();
                    Skill.character = character;
                    Skill.skill = skillId.id;
                    Skill.Max = skillId.max;
                    Skill.value = skillId.min;
                    Skill.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            // Select skill combo (race + class)
            foreach (raceClass skillId in XmlManager.GetRaceStats(character.Race).classes)
            {
                if (skillId.id == character.Class.ToString())
                {
                    foreach (raceClassSkill skill2Id in skillId.skills)
                    {
                        using (var scope = new DataAccessScope())
                        {
                            var Skill = this.model.CharactersSkill.Create();
                                Skill.character = character;
                                Skill.skill = skill2Id.id;
                                Skill.Max = skill2Id.max;
                                Skill.value = skill2Id.min;
                                Skill.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }

            return;
        }
    }
}
