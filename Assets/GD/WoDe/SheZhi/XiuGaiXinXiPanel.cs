using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;
using LM_Workspace;
namespace GD {
    public class XiuGaiXinXiPanel : Panel {
        public UIEventListener closeBtn;
        public InputField nameInput;
        public Dropdown roleSelect;
        public Dropdown areaSelect_p;
        public Dropdown areaSelect_c;
        public Dropdown areaSelect_a;
        public UIEventListener commitBtn;

        private List<Dropdown.OptionData> roleTypeList = new List<Dropdown.OptionData>();
        private List<int> cityData = new List<int>();
        private List<int> areaData = new List<int>();
      
        private void Awake() {
            areaSelect_p.onValueChanged.AddListener( OnProvinceValueChange);
            areaSelect_c.onValueChanged.AddListener(OnCityValueChange);

            commitBtn.onClick = OnClickSubmit;
            closeBtn.onClick = OnClickClose;
            InitRoleSelectData();
        }    

        public override void Show() {
            nameInput.text = "";
            roleSelect.value = 0;
            areaSelect_p.value = 0;
            areaSelect_c.value = 0;
            areaSelect_a.value = 0;
            gameObject.SetActive(true);
            InitProvinceData();
        }

        public override void Hide() {
            gameObject.SetActive(false);
            UIManager.instance.BackToLastPanel();
        }

        public void OnClickClose(GameObject go, PointerEventData data) {
            Hide();          
        }

        public void OnClickSubmit(GameObject go, PointerEventData args) {
            if (string.IsNullOrEmpty(nameInput.text)) {
                UIManager.instance.ShowLoggerMsg("请输入姓名");
                return;
            }         

            if (cityData.Count == 0||areaData.Count==0) {
                UIManager.instance.ShowLoggerMsg("请选择你的区域");
                return;
            }          

            string url = "http://" +LM_Workspace.UtilManager.getInstance.serverIP + "/User/modifyInfo";
            string arg = string.Format("name={0}&role={1}&area_id={2}", nameInput.text, roleSelect.value + 1, areaData[areaSelect_a.value]);
            InterfaceManager instance= GameObject.Find("Camera").GetComponent<InterfaceManager>();
            StartCoroutine(instance.Post(url,arg, (LitJson.JsonData data) => {
                if (((int)data["status"]) != 1) {
                    Util.ShowErrorMessage((int)data["status"]);
                }
                else {
                    LM_Workspace.UtilManager.getInstance.CallOtherAppError("修改成功", () => {
                        gameObject.SetActive(false);
                        UserData.instance.name = nameInput.text;
                        UserData.instance.area = areaData[areaSelect_a.value];
                        UserData.instance.role = roleSelect.value + 1;
                        UtilManager.getInstance.areaID = areaData[areaSelect_a.value];
                        GD.UIManager.instance.OpenMainPanel();
                       // EventManager.instance.NotifyEvent(XMWorkspace.Event.GetBaseInfo, true);
                    });
                }
            }));
        }

        /// <summary>
        /// 初始化角色选择数据
        /// </summary>
        private void InitRoleSelectData() {
            roleTypeList = new List<Dropdown.OptionData>();
            roleTypeList.Add(new Dropdown.OptionData("经销商"));
            roleTypeList.Add(new Dropdown.OptionData("水工"));
            roleTypeList.Add(new Dropdown.OptionData("试压员"));

            roleSelect.ClearOptions();
            roleSelect.AddOptions(roleTypeList);
            roleSelect.value = 0;
        }

        public void OnProvinceValueChange(int value) {            
            int id = UtilManager.provinceDataList[value].id;
            InitCityData(id);
            if (cityData.Count == 0)
                InitAreaData(-1);
            else
                InitAreaData(cityData[0]);
        }

        public void OnCityValueChange(int value) {
            if (cityData.Count == 0)
                return;
            int id = cityData[value];
            InitAreaData(id);
        }

        /// <summary>
        /// 初始化省份数据
        /// </summary>
        public void InitProvinceData() {
            List<Dropdown.OptionData> tmpData = new List<Dropdown.OptionData>();
            for (int i = 0; i <UtilManager.provinceDataList.Count; i++) {
                tmpData.Add(new Dropdown.OptionData(UtilManager.provinceDataList[i].title));
            }

            areaSelect_p.ClearOptions();
            areaSelect_p.AddOptions(tmpData);
            areaSelect_p.value = 0;

            InitCityData(-1);
            InitAreaData(-1);
        }

        /// <summary>
        /// 初始化城市数据
        /// </summary>
        /// <param name="parentID"></param>
        public void InitCityData(int parentID) {
            List<Dropdown.OptionData> tmpData = new List<Dropdown.OptionData>();
            cityData.Clear();
            for (int i = 0; i < UtilManager.cityDataList.Count; i++) {
                if (UtilManager.cityDataList[i].parent == parentID) {
                    cityData.Add(UtilManager.cityDataList[i].id);
                    tmpData.Add(new Dropdown.OptionData(UtilManager.cityDataList[i].title));
                }
            }

            areaSelect_c.ClearOptions();
            areaSelect_c.AddOptions(tmpData);
            if (tmpData.Count == 0) {
                areaSelect_c.options.Add(new Dropdown.OptionData("-"));
            }

            areaSelect_c.value = 0;
            areaSelect_c.RefreshShownValue();
        }

        /// <summary>
        /// 初始化区域数据
        /// </summary>
        /// <param name="parentID"></param>
        public void InitAreaData(int parentID) {
            List<Dropdown.OptionData> tmpData = new List<Dropdown.OptionData>();
            areaData.Clear();
            for (int i = 0; i < UtilManager.areaDataList.Count; i++) {
                if (UtilManager.areaDataList[i].parent == parentID) {
                    areaData.Add(UtilManager.areaDataList[i].id);
                    tmpData.Add(new Dropdown.OptionData(UtilManager.areaDataList[i].title));
                }
            }

            areaSelect_a.ClearOptions();
            areaSelect_a.AddOptions(tmpData);

            if (tmpData.Count == 0) {
                areaSelect_a.options.Add(new Dropdown.OptionData("-"));
            }
            areaSelect_a.value = 0;
            areaSelect_a.RefreshShownValue();
        }
    }
}
