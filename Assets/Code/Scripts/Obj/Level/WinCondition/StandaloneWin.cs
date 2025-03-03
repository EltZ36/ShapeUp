using UnityEngine;
using UnityEngine.EventSystems;

// make sure to import this module into your script
namespace StandAloneWin
{
    /// <summary>
    /// Send this message when you want your level to be 'completed'
    /// Check out CutTheRope win for demo on how to notify this namespace on event
    /// </summary>
    interface IStandAloneWinEvent : IEventSystemHandler
    {
        void OnWin();
    }

    interface IStandAloneWin
    {
        /// <summary>
        /// Calls the event dispatcher to notify module about win condition
        /// </summary>
        public void Invoke();
    }

    public class StandaloneWin : MonoBehaviour, IStandAloneWinEvent
    {
        public GameObject ob;

        void Awake()
        {
            ob.SetActive(false);
        }

        public void OnWin()
        {
            ob.SetActive(true);
        }
    }
}
