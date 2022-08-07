using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XMWorkspace;
using LM_Workspace;

namespace GD
{
    /// <summary>
    /// 区域选择界面
    /// </summary>
    public class UserAreaSelectPanel : MonoBehaviour
    {
        /// <summary>
        /// 登录按钮
        /// </summary>
        private UIEventListener login;
        public Dropdown areaSelect_p;
        public Dropdown areaSelect_c;
        public Dropdown areaSelect_a;
        private List<int> cityData = new List<int>();
        private List<int> areaData = new List<int>();

        private void Start()
        {

            login = transform.Find("btn_login").GetComponent<UIEventListener>();
            login.onClick = SetArea;

            //shuiGong = transform.Find("内容区").Find("btn_shuigong").GetComponent<UIEventListener>();
            //shiYaYuan = transform.Find("内容区").Find("btn_shiyayuan").GetComponent<UIEventListener>();


            //jingXiaoShang.onClick = delegate { SetRole(1); };
            //shuiGong.onClick = delegate { SetRole(2); };
            //shiYaYuan.onClick = delegate { SetRole(3); };\
            EventManager.instance.RegisterEvent(XMWorkspace.Event.SetArea, OnSetArea);

            areaSelect_p.onValueChanged.AddListener(OnProvinceValueChange);
            areaSelect_c.onValueChanged.AddListener(OnCityValueChange);
        }

        private void Update()
        {

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Show();
            //}
        }
        /// <summary>
        /// 上传选择区域
        /// </summary>
        private void SetArea(GameObject go, PointerEventData data)
        {
            if (cityData.Count == 0 || areaData.Count == 0) {
                UIManager.instance.ShowLoggerMsg("请选择你的区域");
                return;
            }

            GameObject.Find("Camera").GetComponent<InterfaceManager>().SetArea(areaData[areaSelect_a.value]);
        }

        /// <summary>
        /// 选择区域回调
        /// </summary>
        /// <param name="obj"></param>
        private void OnSetArea(object[] obj)
        {
           
            if ((bool)obj[0])
            {
                ///设置成功
                UIManager.instance.OpenMainPanel(gameObject);
            }
            else
            {
                ///设置失败
                UIManager.instance.ShowLoggerMsg("区域设置出错");
            }


        }

       

        private int pageCount=50;
        public void Show()
        {
            gameObject.SetActive(true);
            areaSelect_p.value = 0;
            areaSelect_c.value = 0;
            areaSelect_a.value = 0;
            InitProvinceData();
            //todo,选区域联动
            //GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAreaList(1, pageCount);
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
            for (int i = 0; i < UtilManager.provinceDataList.Count; i++) {
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