using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Micromind.DataControls.FormDesigner
{
	public class DescriberObject : ICustomTypeDescriptor
	{
		private object obj;

		public DescriberObject(object obj)
		{
			this.obj = obj;
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(obj, noCustomTypeDesc: true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(obj, attributes, noCustomTypeDesc: true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(obj, noCustomTypeDesc: true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(obj, noCustomTypeDesc: true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return obj;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(obj, noCustomTypeDesc: true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection propertyDescriptorCollection = (!(obj is ICustomTypeDescriptor)) ? TypeDescriptor.GetProperties(obj, noCustomTypeDesc: true) : (obj as ICustomTypeDescriptor).GetProperties();
			PropertyDescriptorCollection propertyDescriptorCollection2 = new PropertyDescriptorCollection(null);
			foreach (PropertyDescriptor item in propertyDescriptorCollection)
			{
				if (DesignerLib.visibleProperties.Contains(item.Name))
				{
					propertyDescriptorCollection2.Add(item);
				}
				else if (DesignerLib.readOnlyProperties.Contains(item.Name))
				{
					ReadOnlyPropertyDescriptor value = new ReadOnlyPropertyDescriptor(item);
					propertyDescriptorCollection2.Add(value);
				}
			}
			return propertyDescriptorCollection2;
		}

		public object GetEditor(Type editorBaseType)
		{
			return new UITypeEditor();
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(obj, noCustomTypeDesc: true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(obj, noCustomTypeDesc: true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(obj, noCustomTypeDesc: true);
		}

		public override string ToString()
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			return string.Empty;
		}
	}
}
