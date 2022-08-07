using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM_Workspace
{
public class EnumManager
	{
		/// <summary>
		/// 预约类型
		/// </summary>
		public enum appoimentType
		{
			None,
			check,
		}
			

		public static int ReturnAreaID(string _city)
		{
			int returnID = 0;
			switch (_city) 
			{
			case "南京市":
				returnID = 1;
				break;
			}
			return returnID;
		}

		public enum footerCloseType
		{
			forum,
			foremanRank,
			createAppoiment,
			market,
			myself,
			other,
		}

		public enum imageSelfType
		{
			market,
			foremanRank,
		}
	}
}
