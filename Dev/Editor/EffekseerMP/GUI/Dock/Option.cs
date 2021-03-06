﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effekseer.GUI.Dock
{
	class Option : DockPanel
	{
		Component.ParameterList paramerterList = null;

		bool isFiestUpdate = true;

		public Option()
		{
			Label = Resources.GetString("Options") + "###Options";
			paramerterList = new Component.ParameterList();
			paramerterList.SetType(typeof(Data.OptionValues));

			Core.OnAfterLoad += OnAfter;
			Core.OnAfterNew += OnAfter;
		}

		public void FixValues()
		{
			paramerterList.FixValues();
		}

		public override void OnDisposed()
		{
			FixValues();

			Core.OnAfterLoad -= OnAfter;
			Core.OnAfterNew -= OnAfter;
		}

		protected override void UpdateInternal()
		{
			if(isFiestUpdate)
			{
				paramerterList.SetValue(Core.Option);
				isFiestUpdate = false;

				Core.Option.GuiLanguage.OnChanged += GuiLanguage_OnChanged;
			}

			paramerterList.Update();
		}

		private void GuiLanguage_OnChanged(object sender, ChangedValueEventArgs e_)
		{
			var e = Core.Option.GuiLanguage.GetValue();

			if (e == Language.Japanese)
			{
				System.Windows.Forms.MessageBox.Show("次回のEffekseer起動時に適応します。");
			}
			else if (e == Language.English)
			{
				System.Windows.Forms.MessageBox.Show("It adapts to the next startup");
			}
		}

		void OnAfter(object sender, EventArgs e)
		{
			paramerterList.SetValue(Core.Option);
		}
	}
}
