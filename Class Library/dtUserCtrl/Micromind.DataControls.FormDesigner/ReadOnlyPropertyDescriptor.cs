using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Micromind.DataControls.FormDesigner
{
	public class ReadOnlyPropertyDescriptor : PropertyDescriptor
	{
		private PropertyDescriptor desc;

		public override AttributeCollection Attributes => desc.Attributes;

		public override Type ComponentType => desc.ComponentType;

		public override string DisplayName => desc.DisplayName;

		public override bool IsReadOnly => true;

		public override string Name => desc.Name;

		public override Type PropertyType => desc.PropertyType;

		public ReadOnlyPropertyDescriptor(PropertyDescriptor desc)
			: base(desc.Name, null)
		{
			this.desc = desc;
		}

		public override bool CanResetValue(object component)
		{
			return true;
		}

		public override object GetValue(object component)
		{
			return new DescriberObject(desc.GetValue(component));
		}

		public override void ResetValue(object component)
		{
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override void SetValue(object component, object value)
		{
		}

		public override object GetEditor(Type editorBaseType)
		{
			return new UITypeEditor();
		}
	}
}
