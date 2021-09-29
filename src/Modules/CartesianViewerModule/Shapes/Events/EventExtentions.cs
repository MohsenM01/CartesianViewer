using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;

namespace CartesianViewerModule.Shapes.Events
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventExtentions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentControl"></param>
        /// <param name="propertyPath"></param>
        /// <param name="eventName"></param>
        public static void SetShapeTrigger(this Shape contentControl, string propertyPath, string eventName)
        {
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (eventName == null)
                throw new ArgumentNullException(nameof(eventName));

            // create the command action and bind the command to it
            var invokeCommandAction = new InvokeCommandAction { CommandParameter = "this" };
            var binding = new Binding { Path = new PropertyPath(propertyPath) };
            BindingOperations.SetBinding(invokeCommandAction, InvokeCommandAction.CommandProperty, binding);

            // create the event trigger and add the command action to it
            var eventTrigger = new Microsoft.Xaml.Behaviors.EventTrigger { EventName = eventName };
            eventTrigger.Actions.Add(invokeCommandAction);

            // attach the trigger to the control
            var triggers = Interaction.GetTriggers(contentControl);
            triggers.Add(eventTrigger);
        }
    }
}
