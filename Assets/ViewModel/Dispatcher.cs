using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;

/// <summary>
/// The dispatcher class that handles the dispatching and thread control of all tasks sent to it.
/// </summary>
public class Dispatcher : MonoBehaviour
{
    /// <summary>
    /// Runs the passed through action
    /// </summary>
    /// <param name="action">
    /// The action that is sent through
    /// </param>
    public static void RunAsync(Action action)
    {
        ThreadPool.QueueUserWorkItem(o => action());
    }

    /// <summary>
    /// Runs both the pass through action and state
    /// </summary>
    /// <param name="action">
    /// The action that is sent through
    /// </param>
    /// <param name="state">
    /// The state that is sent through
    /// </param>
    public static void RunAsync(Action<object> action, object state)
    {
        ThreadPool.QueueUserWorkItem(o => action(o), state);
    }

    /// <summary>
    /// Signals the action to be ran on the main thread
    /// </summary>
    /// <param name="action">
    /// The action that is sent through
    /// </param>
    public static void RunOnMainThread(Action action)
    {
        lock (_backlog)
        {
            _backlog.Add(action);
            _queued = true;
        }
    }

    /// <summary>
    /// Initializes the instance of the gameObject
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (_instance == null)
        {
            _instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
            DontDestroyOnLoad(_instance.gameObject);
        }
    }

    /// <summary>
    /// While active, performs the management of tasks and what threads they're active on.
    /// </summary>
    private void Update()
    {
        if (_queued)
        {
            lock (_backlog)
            {
                var tmp = _actions;
                _actions = _backlog;
                _backlog = tmp;
                _queued = false;
            }

            foreach (var action in _actions)
                action();

            _actions.Clear();
        }
    }

    /// <summary>
    /// Static Instance of the Dispatcher
    /// </summary>
    static Dispatcher _instance;
    /// <summary>
    /// Static bool that declares the queued task or action
    /// </summary>
    static volatile bool _queued = false;
    /// <summary>
    /// Static list that records the backlog of the Dispatcher
    /// </summary>
    static List<Action> _backlog = new List<Action>(8);
    /// <summary>
    /// Static list that records the actions of the Dispatcher
    /// </summary>
    static List<Action> _actions = new List<Action>(8);
}