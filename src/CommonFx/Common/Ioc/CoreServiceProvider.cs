﻿using System;
using System.Collections.Generic;
using CommonServiceLocator;

namespace CommonFx.Common.Ioc
{
    /// <summary>
    /// Core Service Locator
    /// </summary>
    public static class CoreServiceProvider
    {
        /// <summary>
        /// 定位服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LocateService<T>()
        {
            var service = Current.GetInstance<T>();
            return service;
        }
        
        /// <summary>
        /// 定位服务列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> LocateServices<T>()
        {
            var services = Current.GetAllInstances<T>();
            return services;
        }

        #region for ioc extensions

        private static Func<IServiceLocator> _currentFunc = () => new Lazy<NullServiceLocator>(() => new NullServiceLocator()).Value;
        /// <summary>
        /// 当前实现Factory，支持运行时替换
        /// </summary>
        public static Func<IServiceLocator> CurrentFunc
        {
            get { return _currentFunc; }
            set { _currentFunc = value; }
        }

        /// <summary>
        /// 当前实现
        /// </summary>
        public static IServiceLocator Current
        {
            get { return _currentFunc(); }
        }


        #endregion
    }
}