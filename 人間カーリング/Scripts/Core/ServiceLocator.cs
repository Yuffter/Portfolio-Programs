using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サービスロケータクラス
/// </summary>
public static class ServiceLocator
{
    // サービスを保持しておく辞書
    private static readonly Dictionary<Type, object> _container;

    static ServiceLocator()
        => _container = new Dictionary<Type, object>();

    /// <summary>
    /// インスタンスを取得する
    /// </summary>
    public static T Resolve<T>()
        => (T)_container[typeof(T)];

    /// <summary>
    /// インスタンスを登録する
    /// </summary>
    public static void Register<T>(T instance)
        => _container[typeof(T)] = instance;

    /// <summary>
    /// インスタンスを登録解除する
    /// </summary>
    public static void UnRegister<T>()
    {
        if (_container.ContainsKey(typeof(T))) _container.Remove(typeof(T));
        else Debug.Log($"{nameof(T)}は登録されていません");
    }

    /// <summary>
    /// 登録しているインスタンスを削除する
    /// </summary>
    public static void Clear<T>()
        => _container.Remove(typeof(T));
}