using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal sealed class PackageManagerDetailTextFieldExtension
        : VisualElement,
          IPackageManagerExtension
    {
        private TextField m_textField;

        static PackageManagerDetailTextFieldExtension()
        {
            var extension = new PackageManagerDetailTextFieldExtension();
            PackageManagerExtensions.RegisterExtension( extension );
        }

        VisualElement IPackageManagerExtension.CreateExtensionUI()
        {
            m_textField = new TextField
            {
                multiline        = true,
                selectAllOnFocus = true,
                style =
                {
                    height = 128,
                },
            };

            return m_textField;
        }

        void IPackageManagerExtension.OnPackageSelectionChange( PackageInfo packageInfo )
        {
            var json      = JsonUtility.ToJson( packageInfo, true );
            var isVisible = !string.IsNullOrWhiteSpace( json );

            m_textField.visible = isVisible;

            if ( !isVisible ) return;

            m_textField.value = json;
        }

        void IPackageManagerExtension.OnPackageAddedOrUpdated( PackageInfo packageInfo )
        {
        }

        void IPackageManagerExtension.OnPackageRemoved( PackageInfo packageInfo )
        {
        }
    }
}