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
                    var spell = this.model.CharactersSpells.Create();
                        spell.character = character;
                        spell.spell = spellid.id;
                        spell.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }
            #endregion

            #region Select spell of class
            foreach (classeSpell spellid in XmlManager.GetClassStats(character.Class).spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var spell = this.model.CharactersSpells.Create();
                        spell.character = character;
                        spell.spell = spellid.id;
                        spell.created_at = ServerDateTime.Now;
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
                            var spell = this.model.CharactersSpells.Create();
                                spell.character = character;
                                spell.spell = spell2Id.id;
                                spell.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }
            #endregion
        }

        internal void GeraSkills(Character character)
        {
            #region Select skill of race
            foreach (raceSkill skillId in XmlManager.GetRaceStats(character.Race).skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var skill = this.model.CharactersSkill.Create();
                        skill.character = character;
                        skill.skill     = skillId.id;
                        skill.Max       = skillId.max;
                        skill.value = skillId.min;
                        skill.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }
            #endregion

            #region Select skill of class
            foreach (classeSkill skillId in XmlManager.GetClassStats(character.Class).skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var skill = this.model.CharactersSkill.Create();
                    skill.character = character;
                    skill.skill = skillId.id;
                    skill.Max = skillId.max;
                    skill.value = skillId.min;
                    skill.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }
            #endregion

            #region Select skill combo (race + class)
            foreach (raceClass skillId in XmlManager.GetRaceStats(character.Race).classes)
            {
                if (skillId.id == character.Class.ToString())
                {
                    foreach (raceClassSkill skill2Id in skillId.skills)
                    {
                        using (var scope = new DataAccessScope())
                        {
                            var skill = this.model.CharactersSkill.Create();
                                skill.character = character;
                                skill.skill = skill2Id.id;
                                skill.Max = skill2Id.max;
                                skill.value = skill2Id.min;
                                skill.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }
            #endregion
        }
    }
}
