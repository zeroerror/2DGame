using Game.CoreBattle.Context;

namespace Game.CoreBattle {

    public class BattleCore {

        BattleContext battleContext;

        public BattleCore(){}

        public void Ctor(){
            battleContext = new BattleContext();
        }

    }

}