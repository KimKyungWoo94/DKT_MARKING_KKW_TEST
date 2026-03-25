using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzIna.DataMatrix
{
    /// <summary>
    /// 싱글톤 패턴을 지원해 주는 추상 클래스
    /// </summary>
    /// <typeparam name="ClassType">싱글톤 패턴을 적용할 클래스 타입</typeparam>
    public abstract class SingleTone<ClassType>where ClassType : class 
    {
        public static bool ExistInstance
        {
            get { return _Instance != null; }
        }
        private static ClassType _Instance = null;
        private static object _SyncObj = new object();
        public static ClassType Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncObj)
                    {
                        if (_Instance == null)
                        {
                            _Instance = Activator.CreateInstance(typeof(ClassType), true) as ClassType; //런타임에 형식(T)의 인스턴스를 생성
                        }
                    }
                }
                return _Instance;
            }
        }
        protected SingleTone()
        {
            OnCreateInstance();
        }     
        /// <summary>
        /// 싱글톤 생성시 발생
        /// </summary>
        protected virtual void OnCreateInstance()
        {

        }
        
    }
}
