# 敌人生成文件使用指北

## 文件的命名方式
首先是文件的命名方式，json 文件名一定需要和当前 scenes 名保持一直，这是能正确访问到关卡对应敌人文件的第一步！

## 数值的填充
数值格式如下所示
```
{
  "rounds": [
    {
      "waves": [
        {
          "enemyPrefabName": "EnemyGI",
          "count": 10,
          "rate": 0.8,
          "interval": 13
        }
      ]
    },
    {
      "waves": [
        {
          "enemyPrefabName": "EnemyArmor",
          "count": 8,
          "rate": 1.5,
          "interval": 9
        },
        {
          "enemyPrefabName": "EnemyMinion",
          "count": 10,
          "rate": 2,
          "interval": 3
        }
      ]
    }
  ]
}
```
如果需要新增一轮敌人，则对应在 rounds 末尾增加一个结构体
```
{
  "waves": [
    {
      "enemyPrefabName": "EnemyArmor",
      "count": 8,
      "rate": 1.5,
      "interval": 9
    }
  ]
}
```
如需在一轮敌人内设置不懂的敌人和数量，则对应在 waves 末尾增加一个结构体
```
{
  "enemyPrefabName": "EnemyMinion",
  "count": 10,
  "rate": 2,
  "interval": 3
}
```
