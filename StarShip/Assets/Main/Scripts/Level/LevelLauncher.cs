using Main.Scripts.Player;
using UnityEngine;

namespace Main.Scripts.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private LevelBorder _borders;
        [SerializeField] private Player.Player _playerPrefab ;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private PlayerInputController _playerInputController;
        private Player.Player _currentPlayer = null;

        private void Start()
        {
            if (_currentPlayer == null)
                SetupPlayer();
            
            _currentPlayer.SetupShip();
            _currentPlayer.EnableShip();
        }

        public void StartLevel()
        {
            _playerInputController.EnableInputHandle();
        }

        public void OnPlayerLose()
        {
            _playerInputController.DisableInputHandle();
        }

        private void SetupPlayer()
        {
            _currentPlayer = Instantiate(_playerPrefab)
                .With(p=>p.transform.position = _startPosition.position)
                .With(p=>p.transform.rotation = Quaternion.identity)
                .With(p=>p.Initialize(_playerInputController,_borders));
        }
    }
}
