using OppositeGame._project.Scripts.GUI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OppositeGame._project.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] StartScreen startScreen;
        [SerializeField] StartScreen continueScreen;
        [SerializeField] StartScreen gameOverScreen;
        [SerializeField] InputActionReference menuAction;
        private void Start()
        {
            startScreen.gameObject.SetActive(true);
            //continueScreen.gameObject.SetActive(false);
            //gameOverScreen.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            menuAction.action.Enable();
            menuAction.action.performed += OnMenuAction;
        }

        private void OnMenuAction(InputAction.CallbackContext obj)
        {
            Debug.Log("Menu Action");
        }

        private void OnDisable()
        {
            menuAction.action.performed -= OnMenuAction;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startScreen.gameObject.SetActive(false);
                continueScreen.gameObject.SetActive(true);
            }
        }
    }
}