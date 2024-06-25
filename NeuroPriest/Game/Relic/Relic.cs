using NeuroPriest.Shared;

/*

CTRL (ArmaChristiList) (mana)

Golden Cross (Blessed) DONE
gain 1 attack next attack

Gospel Fragment (Blessed)
Root all enemies for 1 turn

Testament of Solomon (Sacred)
Disable all interatables for 1 turn

Blood of Christ (Divine)
Gain invulnerablity for the next move

SHIFT (Penance) (once)

Blessing of Christ (Blessed)
Gain invulnerability for 3 turns

Eucharist (Blessed) DONE
Regen full mana

Veil of Veronica (Sacred)
Move twice in a row

Crown of Thorns (Divine)
1 dmg to everyone in room


PASSIVE (Blessing)

Tabernacle (Blessed) DONE
ArmaChristiList Abilites cost 1 less (min 1)

Holy Water (Blessed) DONE
faith Regeneration every round

Cain's Sickle (Sacred)
Gain attack everytime you move up to max of 3

True Cross (Divine)
When you die go back to spawn point but have 0 faith
*/

namespace NeuroPriest.Relics
{
    internal abstract class Relic : StaticInit
    {
        public abstract string Image { get; }
        public abstract string Desc { get; }
        public abstract string Effect { get; }
    }
}
