using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RA.CommandConsole
{
    public class Console : MonoBehaviour
    {
        [SerializeField] private InputField inputfield;
        [SerializeField] private RectTransform outputRoot;
        [SerializeField] private Text outputText;

        private bool _showOutputText = true;

        private bool IsActive => inputfield.gameObject.activeInHierarchy;


        private void Awake()
        {
            if(Commands.main == null)
            {
                Commands.main = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.F8))
            {
                Show(!IsActive);
            }
        }

        public void Show(bool active)
        {
            inputfield.gameObject.SetActive(active);
            if(!active || _showOutputText)
            {
                outputRoot.gameObject.SetActive(active);
            }
        }

        public void OnRetun()
        {
            var x = Commands.HandleInput(inputfield.text);
            if (x)
                Commands.Log("The  <b>" + inputfield.text + "</b> command was executed successfully.");
            else
                Commands.Log("The  <b>" + inputfield.text + "</b> command does not exist.");
            inputfield.text = "";
        }

        internal void Log(string log) // << esta funcion se puede mejroar para que maneje objetos texto en vez de solo un string
        {
            outputText.text += "\n" + log;
        }


    }
}
