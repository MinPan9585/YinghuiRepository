# ���������ļ�ʹ��ָ��

## �ļ���������ʽ
�������ļ���������ʽ��json �ļ���һ����Ҫ�͵�ǰ scenes ������һֱ����������ȷ���ʵ��ؿ���Ӧ�����ļ��ĵ�һ����

## ��ֵ�����
��ֵ��ʽ������ʾ
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
�����Ҫ����һ�ֵ��ˣ����Ӧ�� rounds ĩβ����һ���ṹ��
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
������һ�ֵ��������ò����ĵ��˺����������Ӧ�� waves ĩβ����һ���ṹ��
```
{
  "enemyPrefabName": "EnemyMinion",
  "count": 10,
  "rate": 2,
  "interval": 3
}
```
