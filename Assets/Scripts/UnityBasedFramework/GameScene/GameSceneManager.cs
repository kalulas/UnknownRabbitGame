#region FILE HEADER
// Filename: GameSceneManager.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.DesignPattern;
using Framework.GameScene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityBasedFramework.GameScene
{
    public class GameSceneManager : Singleton<GameSceneManager>
    {
        private BaseGame m_CurrentGame;
        private GameFrameTicker m_GameFrameTicker;
        private GameFrameUpdate m_GameFrameUpdate;
        private Dictionary<string, CancellationTokenSource> m_LoadSceneAsyncSourceDict;
        private Dictionary<string, Coroutine> m_LoadSceneCoroutineDict;

        #region Properties

        public BaseGame CurrentGame => m_CurrentGame;

        #endregion

        #region Singleton
        
        private GameSceneManager()
        {
            
        }

        public override void OnSingletonInit()
        {
            m_LoadSceneAsyncSourceDict = new Dictionary<string, CancellationTokenSource>();
            m_LoadSceneCoroutineDict = new Dictionary<string, Coroutine>();
            m_GameFrameUpdate = OnGameFrameUpdate;
        }

        public override void OnSingletonDisposed()
        {
            m_LoadSceneAsyncSourceDict.Clear();
            m_LoadSceneAsyncSourceDict = null;
            m_LoadSceneCoroutineDict.Clear();
            m_LoadSceneCoroutineDict = null;
        }

        #endregion

        #region Event Function

        // public void OnCallerStart()
        // {
        //     
        // }

        private void OnGameFrameUpdate(uint frameCount)
        {
            m_CurrentGame?.FrameUpdate(frameCount);
        }

        public void OnCallerUpdate(float deltaTime)
        {
            m_CurrentGame?.Update(deltaTime);
        }

        public void OnCallerFixedUpdate(float fixedDeltaTime)
        {
            m_GameFrameTicker?.Tick(fixedDeltaTime);
        }

        // public void OnCallerDestroy()
        // {
        //     
        // }

        #endregion

        #region Private Utils

        private void ExitPreviousGame()
        {
            m_GameFrameTicker = null;
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Exit();
                m_CurrentGame = null;
            }
        }

        private async Task PerformSceneLoading(CancellationToken token, string sceneName)
        {
            token.ThrowIfCancellationRequested();
            if (token.IsCancellationRequested)
            {
                return;
            }

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            while (true)
            {
                token.ThrowIfCancellationRequested();
                if (token.IsCancellationRequested)
                {
                    return;
                }

                // instead of asyncOperation.progress
                if (asyncOperation.isDone)
                {
                    break;
                }
            }

            asyncOperation.allowSceneActivation = true;
            token.ThrowIfCancellationRequested();
        }

        private IEnumerator PerformSceneLoading<TGame>(string sceneName) where TGame : BaseGame
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            while (asyncOperation.progress < 0.9f)
            {
                yield return null;
            }

            asyncOperation.allowSceneActivation = true;
            StartNewGame<TGame>();
        }

        #endregion

        #region Public Interface

        public void StartNewGame<TGame>() where TGame : BaseGame
        {
            ExitPreviousGame();
            m_CurrentGame = Activator.CreateInstance<TGame>();
            m_CurrentGame.Init();
            m_CurrentGame.Start();
            
            var frameLength = m_CurrentGame.GetFrameLength();
            m_GameFrameTicker = GameFrameTicker.Create(frameLength, m_GameFrameUpdate);
        }

        /// <summary>
        /// use UnityEngine.SceneManagement.SceneManager.LoadSceneAsync for async loading, 
        /// <see cref="StartNewGame{TGame}"/> is called after scene loaded
        /// </summary>
        /// <param name="sceneIdentifier"></param>
        /// <typeparam name="TGame"></typeparam>
        public async Task StartNewGameWithScene<TGame>(string sceneIdentifier) where TGame : BaseGame
        {
            if (!m_LoadSceneAsyncSourceDict.ContainsKey(sceneIdentifier))
            {
                var cts = new CancellationTokenSource();
                m_LoadSceneAsyncSourceDict.Add(sceneIdentifier, cts);
                try
                {
                    await PerformSceneLoading(cts.Token, sceneIdentifier);
                    StartNewGame<TGame>();
                }
                catch (OperationCanceledException ex)
                {
                    if (ex.CancellationToken == cts.Token)
                    {
                        Debug.Log($"[GameSceneManager.StartNewGameWithScene] {cts.Token} is cancelled");
                        // TODO after cancellation
                    }
                }
                finally
                {
                    cts.Cancel();
                    m_LoadSceneAsyncSourceDict.Remove(sceneIdentifier);
                }
            }
            else
            {
                Debug.LogWarning($"[GameSceneManager.StartNewGameWithScene] {sceneIdentifier} is already loading");
            }
        }

        public void CancelNewGameSceneLoading(string sceneIdentifier)
        {
            if (m_LoadSceneAsyncSourceDict.TryGetValue(sceneIdentifier, out var source))
            {
                source.Cancel();
                m_LoadSceneAsyncSourceDict.Remove(sceneIdentifier);
                Debug.Log($"[GameSceneManager.CancelNewGameSceneLoading] scene loading of {sceneIdentifier} cancelled");
            }
        }

        /// <summary>
        /// Coroutine based implementation
        /// </summary>
        /// <param name="sceneIdentifier"></param>
        /// <param name="coroutineHost"></param>
        /// <typeparam name="TGame"></typeparam>
        public void StartNewGameWithSceneCoroutine<TGame>(string sceneIdentifier, MonoBehaviour coroutineHost) where TGame : BaseGame
        {
            if (!m_LoadSceneCoroutineDict.ContainsKey(sceneIdentifier))
            {
                var cor = coroutineHost.StartCoroutine(PerformSceneLoading<TGame>(sceneIdentifier));
                m_LoadSceneCoroutineDict.Add(sceneIdentifier, cor);
            }
            else
            {
                Debug.LogWarning($"[GameSceneManager.StartNewGameWithSceneCoroutine] {sceneIdentifier} is already loading");
            }
        }

        public void CancelNewGameSceneLoadingCoroutine(string sceneIdentifier, MonoBehaviour coroutineHost)
        {
            if (m_LoadSceneCoroutineDict.TryGetValue(sceneIdentifier, out var coroutine))
            {
                coroutineHost.StopCoroutine(coroutine);
            }
        }

        #endregion
    }
}