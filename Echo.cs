using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ateam
{
    public class Echo : BaseBattleAISystem
    {
        //---------------------------------------------------
        // InitializeAI
        //---------------------------------------------------
        override public void InitializeAI()
        {
        }

        //---------------------------------------------------
        // UpdateAI
        //---------------------------------------------------
        override public void UpdateAI()
        {
            //ステージデータ取ってくるときのサンプル
            int[,] stageData = GetStageData();

            for (int y = 0; y < stageData.GetLength(0); y++)
            {
                for (int x = 0; x < stageData.GetLength(1); x++)
                {
                    //Debug.Log("stage" + y + " : " + x );

                    //通常ブロック
                    if (stageData[y, x] == (int)Define.Stage.BLOCK_TYPE.NORMAL)
                    {
                        //Debug.Log("通常ブロック");        
                    }
                    else if (stageData[y, x] == (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                    {
                        //Debug.Log("障害物ブロック");
                    }
                    else if (stageData[y, x] == (int)Define.Stage.BLOCK_TYPE.NONE)
                    {
                        //Debug.Log("ブロックなし");
                    }
                }
            }

            //自チームのデータを取得
            List<CharacterModel.Data> playerList = GetTeamCharacterDataList(TEAM_TYPE.PLAYER);

            //敵チームのデータ取得
            List<CharacterModel.Data> enemyList = GetTeamCharacterDataList(TEAM_TYPE.ENEMY);

            //移動のサンプル
            for (int i = 0; i < playerList.Count; i++)
            {
                CharacterModel.Data character = playerList[i];
                int id = character.ActorId;

                //自分がいるステージのブロックタイプを取得
                Define.Stage.BLOCK_TYPE type = GetBlockType(character.BlockPos);

                //敵用
                CharacterModel.Data EnemyCharacter = enemyList[i];
                int nEnemyId = EnemyCharacter.ActorId;
                //敵デバッグ
                //Debug.Log("EnemyID:"+ nEnemyId + "EnemyPos(X):" + EnemyCharacter.BlockPos.x + "EnemyPos(Y):" + EnemyCharacter.BlockPos.y);

                //プレイヤーとエネミーの距離取得
                float fDis = Vector2.Distance(character.BlockPos, EnemyCharacter.BlockPos);
                //Debug.Log("Distance:" + fDis);

                //今現在の体力値
                float fLife = (float)character.Hp / character.MaxHp;

                         //itemある時(取られてても行ってしまう)
                if(isItem==true &&fLife < 0.8f&&i==0)
                {
                    if(itemPos.x==playerList[0].BlockPos.x&& itemPos.y == playerList[0].BlockPos.y)
                    {
                        isItem = false;
                    }

                    Action(id, Define.Battle.ACTION_TYPE.ATTACK_LONG);
                    //x揃える
                    if (itemPos.x != playerList[0].BlockPos.x)
                    {
                        if(playerList[0].BlockPos.x<itemPos.x&&stageData[(int)playerList[0].BlockPos.x+1, (int)playerList[0].BlockPos.y]!= (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                        {
                            Move(id, Common.MOVE_TYPE.RIGHT);
                        }
                        else if(playerList[0].BlockPos.x < itemPos.x && stageData[(int)playerList[0].BlockPos.x + -1, (int)playerList[0].BlockPos.y] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                        {
                            Move(id, Common.MOVE_TYPE.LEFT);
                        }
                        else
                        {
                            int move = UnityEngine.Random.Range(0, 4);
                            switch (move)
                            {
                                case 0:
                                    //上移動
                                    Move(id, Common.MOVE_TYPE.UP);
                                    break;

                                case 1:
                                    //下移動
                                    Move(id, Common.MOVE_TYPE.DOWN);
                                    break;

                                case 2:
                                    //左移動
                                    Move(id, Common.MOVE_TYPE.LEFT);
                                    break;

                                case 3:
                                    //右移動
                                    Move(id, Common.MOVE_TYPE.RIGHT);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (playerList[0].BlockPos.y < itemPos.y && stageData[(int)playerList[0].BlockPos.x, (int)playerList[0].BlockPos.y+1] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                        {
                            Move(id, Common.MOVE_TYPE.UP);
                        }
                        else if (playerList[0].BlockPos.y > itemPos.y && stageData[(int)playerList[0].BlockPos.x , (int)playerList[0].BlockPos.y-1] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                        {
                            Move(id, Common.MOVE_TYPE.DOWN);
                        }
                        else
                        {
                            int move = UnityEngine.Random.Range(0, 4);
                            switch (move)
                            {
                                case 0:
                                    //上移動
                                    Move(id, Common.MOVE_TYPE.UP);
                                    break;

                                case 1:
                                    //下移動
                                    Move(id, Common.MOVE_TYPE.DOWN);
                                    break;

                                case 2:
                                    //左移動
                                    Move(id, Common.MOVE_TYPE.LEFT);
                                    break;

                                case 3:
                                    //右移動
                                    Move(id, Common.MOVE_TYPE.RIGHT);
                                    break;
                            }
                        }
                    }


                }
               // エノキド担当（10割～8割）
               else if (fLife >= 0.8f)
                {
                    if (character.BlockPos.x < EnemyCharacter.BlockPos.x)
                    {
                        //右移動
                        Move(id, Common.MOVE_TYPE.RIGHT);
                        Debug.Log("right move");
                    }
                    else if (character.BlockPos.x > EnemyCharacter.BlockPos.x)
                    {
                        //左移動
                        Move(id, Common.MOVE_TYPE.LEFT);
                        Debug.Log("left move");
                    }
                    else if (character.BlockPos.y < EnemyCharacter.BlockPos.y)
                    {
                        //上移動
                        Move(id, Common.MOVE_TYPE.UP);
                        Debug.Log("up move");
                    }
                    else if (character.BlockPos.y > EnemyCharacter.BlockPos.y)
                    {
                        //下移動
                        Move(id, Common.MOVE_TYPE.DOWN);
                        Debug.Log("down move");
                    }

                    //アクションのサンプル
                    if (fDis >= 3) {
                        Action (id, Define.Battle.ACTION_TYPE.ATTACK_SHORT);
                    }
                    else if (fDis >=7) {//中距離攻撃
                        Action (id, Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                    } else if (fDis >= 10){
                        //長距離攻撃
                        Action (id, Define.Battle.ACTION_TYPE.ATTACK_LONG);
                    }
                    else {
                       //無敵アクション
                        Action(id, Define.Battle.ACTION_TYPE.INVINCIBLE);
                        break;
                    }
                }
                //タカハシ担当（8割～3割）
                else if (fLife >= 0.3f)
                {
                    if (fDis <= 5.0f)
                    {
                        Debug.Log("Distance Flag");

                        if (character.BlockPos.x < EnemyCharacter.BlockPos.x)
                        {
                            //右移動
                            Move(id, Common.MOVE_TYPE.RIGHT);
                            Debug.Log("right move");
                        }
                        else if (character.BlockPos.x > EnemyCharacter.BlockPos.x)
                        {
                            //左移動
                            Move(id, Common.MOVE_TYPE.LEFT);
                            Debug.Log("left move");
                        }
                        else if (character.BlockPos.y < EnemyCharacter.BlockPos.y)
                        {
                            //上移動
                            Move(id, Common.MOVE_TYPE.UP);
                            Debug.Log("up move");
                        }
                        else if (character.BlockPos.y > EnemyCharacter.BlockPos.y)
                        {
                            //下移動
                            Move(id, Common.MOVE_TYPE.DOWN);
                            Debug.Log("down move");
                        }
                    }
                    else
                    {
                        int move = UnityEngine.Random.Range(0, 4);
                        switch (move)
                        {
                            case 0:
                                //上移動
                                Move(id, Common.MOVE_TYPE.UP);
                                break;

                            case 1:
                                //下移動
                                Move(id, Common.MOVE_TYPE.DOWN);
                                break;

                            case 2:
                                //左移動
                                Move(id, Common.MOVE_TYPE.LEFT);
                                break;

                            case 3:
                                //右移動
                                Move(id, Common.MOVE_TYPE.RIGHT);
                                break;
                        }
                    }

                    //アクションのサンプル
                    switch (UnityEngine.Random.Range(0, 3))
                    {
                        case 0:
                            //中距離攻撃
                            Action(id, Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                            break;
                        case 1:
                            //長距離攻撃
                            Action(id, Define.Battle.ACTION_TYPE.ATTACK_LONG);
                            break;
                        case 2:
                            //無敵アクション
                            Action(id, Define.Battle.ACTION_TYPE.INVINCIBLE);
                            break;
                    }
                }
                //ミカド担当（3割～0割）
                else
                {
                   //直線に敵がいたら逃げる
                    bool isStraight = false;//ストレートにいるか
                    bool isHorizontal = false;//方向、trueなら水平上
                    int enemyId = 0;
                    int distance = 100;//近さ、近いほど逃げる

                    for (int enemyCount = 0; enemyCount < enemyList.Count; enemyCount++)
                    {
                        if (playerList[i].BlockPos.x == enemyList[enemyCount].BlockPos.x)//縦一列
                        {
                            float temp = playerList[i].BlockPos.y - enemyList[enemyCount].BlockPos.y;
                            //近かったら更新
                            if (distance > Mathf.Abs(temp))
                            {
                                isHorizontal = false;
                                distance = (int)Mathf.Abs(temp);
                                isStraight = true;
                                enemyId = enemyCount;
                                Debug.Log("x軸");
                            }
                        }
                        else if (playerList[i].BlockPos.y == enemyList[enemyCount].BlockPos.y)//横一列
                        {
                            float temp = playerList[i].BlockPos.y - enemyList[enemyCount].BlockPos.y;

                            if (distance > Mathf.Abs(temp))
                            {
                                Debug.Log("y軸");
                                isHorizontal = true;
                                distance = (int)Mathf.Abs(temp);
                                isStraight = true;
                                enemyId = enemyCount;
                            }
                        }
                    }

                    if (isStraight == true)
                    {
                        if (isHorizontal == true)//水平のとき
                        {
                            //上下に逃げ道が無ければ敵と逆に、それも無理なら敵と逆方向
                            if (playerList[i].BlockPos.y < stageData.GetLength(0) - 1 &&
                                stageData[(int)playerList[i].BlockPos.x, (int)playerList[i].BlockPos.y + 1] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                            {
                                Move(id, Common.MOVE_TYPE.UP);
                            }
                            else if (playerList[i].BlockPos.y != 0 && stageData[(int)playerList[i].BlockPos.x, (int)playerList[i].BlockPos.y - 1] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                            {

                                Move(id, Common.MOVE_TYPE.DOWN);
                            }
                            else
                            {
                                if (playerList[i].BlockPos.x < stageData.GetLength(1) - 1 && enemyList[enemyId].BlockPos.x < playerList[i].BlockPos.x
                                    && stageData[(int)playerList[i].BlockPos.x + 1, (int)playerList[i].BlockPos.y] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                                    Move(id, Common.MOVE_TYPE.RIGHT);
                                else
                                    Move(id, Common.MOVE_TYPE.LEFT);
                            }
                        }
                        else
                        {

                            //左右に逃げ道が無ければ敵と逆に
                            //→
                            if (playerList[i].BlockPos.x < stageData.GetLength(1) - 1 &&
                                stageData[(int)playerList[i].BlockPos.x + 1, (int)playerList[i].BlockPos.y] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                            {
                                Move(id, Common.MOVE_TYPE.RIGHT);
                            }
                            else if (playerList[i].BlockPos.x != 0 && stageData[(int)playerList[i].BlockPos.x - 1, (int)playerList[i].BlockPos.y] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                            {
                                Move(id, Common.MOVE_TYPE.LEFT);
                            }
                            else
                            {
                                if (playerList[i].BlockPos.y < stageData.GetLength(0) - 1 && enemyList[enemyId].BlockPos.y < playerList[i].BlockPos.y
                                    && stageData[(int)playerList[i].BlockPos.x, (int)playerList[i].BlockPos.y + 1] != (int)Define.Stage.BLOCK_TYPE.OBSTACLE)
                                    Move(id, Common.MOVE_TYPE.UP);
                                else
                                    Move(id, Common.MOVE_TYPE.DOWN);
                            }

                        }
                    }
                    else//一旦ランダム　//近い奴から逃げるようにする（時間あれば
                    {
                        int move = UnityEngine.Random.Range(0, 4);
                        switch (move)
                        {
                            case 0:
                                //上移動
                                Move(id, Common.MOVE_TYPE.UP);
                                break;

                            case 1:
                                //下移動
                                Move(id, Common.MOVE_TYPE.DOWN);
                                break;

                            case 2:
                                //左移動
                                Move(id, Common.MOVE_TYPE.LEFT);
                                break;

                            case 3:
                                //右移動
                                Move(id, Common.MOVE_TYPE.RIGHT);
                                break;
                        }
                    }

                    //無敵できる状態だったら1%で無敵になる
                    //フレームごとにやってしまってるので1%で十分
                    //今できるかどうか取得できないので、
                    if (Random.Range(0, 100) < 1)
                    {
                        Action(id, Define.Battle.ACTION_TYPE.INVINCIBLE);
                    }
                    //判定できないのでいつも発射
                    //攻撃しながら逃げる
                    ////アクションのサンプル
                    switch (UnityEngine.Random.Range(0, 3))
                    {
                        case 0:
                            //近距離攻撃
                            Action(id, Define.Battle.ACTION_TYPE.ATTACK_SHORT);
                            break;
                        case 1:
                            //中距離攻撃
                            Action(id, Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                            break;
                        case 2:
                            //長距離攻撃
                            Action(id, Define.Battle.ACTION_TYPE.ATTACK_LONG);
                            break;
                    }
                }    
            }
        }

        //---------------------------------------------------
        // ItemSpawnCallback
        //---------------------------------------------------
            Vector2 itemPos;//アイテム保存
        bool isItem = false;

     
        override public void ItemSpawnCallback(ItemSpawnData itemData)
        {
            if (itemData.ItemType == ItemData.ITEM_TYPE.ATTACK_UP)
            {
                //Debug.Log("攻撃力アップアイテム");
            }
            else if (itemData.ItemType == ItemData.ITEM_TYPE.SPEED_UP)
            {
                //Debug.Log("スピードアップアイテム");
            }
            else if (itemData.ItemType == ItemData.ITEM_TYPE.HP_RECOVER)
            {
                //Debug.Log("回復アイテム");
                isItem = true;
                itemPos = itemData.BlockPos;
            }

            //生成された位置
            //Debug.Log(itemData.BlockPos);
        }
    }
}
