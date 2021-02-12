using System;
[Serializable]
public struct Good
{
    public string id;
    public string name;
}

[Serializable]
public struct ResponseGoods
{
    public Good[] Goods;
}

[Serializable]
public struct ResponseUser
{
    public string id;
    public string login;
    public string password;
    public bool admin;
}
