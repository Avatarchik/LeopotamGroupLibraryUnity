﻿//-------------------------------------------------------
// LeopotamGroupLibrary for unity3d
// Copyright (c) 2012-2016 Leopotam <leopotam@gmail.com>
//-------------------------------------------------------

using LeopotamGroup.Common;
using LeopotamGroup.Gui.Interaction;
using LeopotamGroup.Gui.Layout;
using LeopotamGroup.Gui.Widgets;
using UnityEngine;

namespace LeopotamGroup.Gui.Common {
    /// <summary>
    /// Contains helpers for widget creation at runtime.
    /// </summary>
    public static class GuiControlFactory {
        static T CreateWidget<T> (Transform parent = null) where T: MonoBehaviour {
            GuiSystem.Instance.Validate ();
            var go = new GameObject (typeof (T).Name);
            go.layer = GuiConsts.DefaultGuiLayer;
            var widget = go.AddComponent<T> ();
            if (parent != null) {
                widget.transform.SetParent (parent, false);
                widget.transform.localPosition = Vector3.zero;
            }
            return widget;
        }

        /// <summary>
        /// Create GuiPanel.
        /// </summary>
        /// <returns>The widget panel.</returns>
        public static GuiPanel CreateWidgetPanel () {
            return CreateWidget<GuiPanel> ();
        }

        /// <summary>
        /// Create GuiSprite.
        /// </summary>
        /// <returns>The widget sprite.</returns>
        public static GuiSprite CreateWidgetSprite () {
            return CreateWidget<GuiSprite> ();
        }

        /// <summary>
        /// Create GuiLabel.
        /// </summary>
        /// <returns>The widget label.</returns>
        public static GuiLabel CreateWidgetLabel () {
            return CreateWidget<GuiLabel> ();
        }

        /// <summary>
        /// Create GuiButton.
        /// </summary>
        /// <returns>The widget button.</returns>
        public static GuiButton CreateWidgetButton () {
            var button = CreateWidget<GuiButton> ();
            button.Visuals = new [] { button.gameObject.AddComponent<GuiSprite> () };
            return button;
        }

        /// <summary>
        /// Create GuiButton with label.
        /// </summary>
        /// <returns>The widget button.</returns>
        public static GuiButton CreateWidgetButtonWithLabel () {
            var button = CreateWidget<GuiButton> ();
            var label = CreateWidget<GuiLabel> (button.transform);
            label.Depth = 1;
            button.Visuals = new GuiWidget[] { button.gameObject.AddComponent<GuiSprite> (), label };
            return button;
        }

        /// <summary>
        /// Create slider.
        /// </summary>
        /// <returns>The widget slider.</returns>
        /// <param name="withInteraction">If set to <c>true</c> with interaction.</param>
        /// <param name="withThumb">If true - thumb subcomponent willbe created.</param>
        public static GuiSlider CreateWidgetSlider (bool withInteraction = false, bool withThumb = false) {
            var slider = CreateWidget<GuiSlider> ();
            var background = CreateWidget<GuiSprite> (slider.transform);
            var foreground = CreateWidget<GuiSprite> (slider.transform);
            foreground.Depth = 1;
            slider.Background = background;
            slider.Foreground = foreground;
            if (withInteraction) {
                background.gameObject.AddComponent<GuiEventReceiver> ();
            }
            if (withThumb) {
                var thumb = CreateWidget<GuiSprite> (slider.transform);
                thumb.Depth = 2;
                var bind = thumb.gameObject.EnsureGetComponent<GuiBindPosition> ();
                bind.Target = foreground;
                bind.Horizontal = 1f;
                bind.Once = false;
            }
            return slider;
        }

        /// <summary>
        /// Create GuiBindPosition.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiBindPosition CreateLayoutBindPosition (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiBindPosition> () : null;
        }

        /// <summary>
        /// Create GuiBindSize.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiBindSize CreateLayoutBindSize (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiBindSize> () : null;
        }

        /// <summary>
        /// Create GuiBindEventReceiverSize.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiBindEventReceiverSize CreateLayoutBindEventReceiverSize (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiBindEventReceiverSize> () : null;
        }

        /// <summary>
        /// Create GuiBindPanelRange.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiBindPanelRange CreateLayoutGuiBindPanelRange (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiBindPanelRange> () : null;
        }

        /// <summary>
        /// Create scroll view.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiScrollView CreateLayoutScrollView (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiScrollView> () : null;
        }

        /// <summary>
        /// Create overlay following to world transform.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiOverlayTransform CreateLayoutOverlayTransform (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiOverlayTransform> () : null;
        }

        /// <summary>
        /// Create component for dragging GuiScrollView content.
        /// </summary>
        /// <param name="go">GameObject.</param>
        public static GuiDragScrollView CreateInteractionDragScrollView (GameObject go) {
            return go != null ? go.EnsureGetComponent<GuiDragScrollView> () : null;
        }
    }
}