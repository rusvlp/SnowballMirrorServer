using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace UnityMirrorExample.Server
{
    public class UGlassesComponent : GlassesComponent
    {
        public GameObject Instance;
        public GameObject Button;
        public GameObject SaveButton;
        public GameObject EditField;
        public DevicesListController DevicesListController;

        public void EditName()
        {
            text_name.SetActive(false);
            Button.SetActive(false);
            SaveButton.SetActive(true);
            EditField.SetActive(true);

        }

        public void FinishEdit()
        {
            text_name.SetActive(true);
          //  Button.SetActive(true);
            SaveButton.SetActive(false);
            EditField.SetActive(false);

            this.Name = EditField.GetComponent<TMP_InputField>().text;
            this.text_name.GetComponent<TMP_Text>().text = this.Name;

            SetKnown();

        }

        public void SetKnown()
        {
            DevicesListController.SetKnown(this.Instance, DevicesListController.parent);
        }
    }
}

