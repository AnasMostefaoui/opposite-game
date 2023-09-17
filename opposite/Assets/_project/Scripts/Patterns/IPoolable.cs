using System;

namespace OppositeGame._project.Scripts.Patterns
{
    public interface IPoolable<T>
    {
        Action<T> OnRelease { 
            get;
            set; 
        }
        
    }
}