# Unity Essentials

This module is part of the Unity Essentials ecosystem and follows the same lightweight, editor-first approach.
Unity Essentials is a lightweight, modular set of editor utilities and helpers that streamline Unity development. It focuses on clean, dependency-free tools that work well together.

All utilities are under the `UnityEssentials` namespace.

```csharp
using UnityEssentials;
```

## Installation

Install the Unity Essentials entry package via Unity's Package Manager, then install modules from the Tools menu.

- Add the entry package (via Git URL)
    - Window → Package Manager
    - "+" → "Add package from git URL…"
    - Paste: `https://github.com/CanTalat-Yakan/UnityEssentials.git`

- Install or update Unity Essentials packages
    - Tools → Install & Update UnityEssentials
    - Install all or select individual modules; run again anytime to update

---

# Info Attribute

> Quick overview: Show contextual HelpBox messages in the Inspector. Use a fixed message via the attribute, or bind the message to a string field’s value. Supports Info/Warning/Error types and adjustable icon size.

A small PropertyDrawer that displays a HelpBox above your field. For non‑string fields, you provide the message via the attribute. For string fields, you can either provide a fixed message (and still edit the string), or omit it and the drawer will use the string’s current value as the message and hide the text field.

![screenshot](Documentation/Screenshot.png)

## Features
- HelpBox above the field with selectable type: Info, Warning, Error, or None
- Two message sources:
  - Attribute constructor message (static text)
  - String field value (dynamic text) when no message is provided
- Auto message type when using string binding: prefixes “Error”/“Warning” select the icon automatically
- Adjustable icon size for minimum box height (`iconSize`, default 32)
- Non‑string fields: help + default field are both drawn
- String fields: 
  - If attribute message is provided → draw help + show the string field normally
  - If attribute message is omitted → draw help using the string value and hide the string field

## Requirements
- Unity Editor 6000.0+ (Editor‑only; attribute lives in Runtime for convenience)
- No external dependencies

Tip: For dynamic messages from gameplay data, use a string serialized field and update it from your scripts; the HelpBox will reflect the current value in the Inspector.

## Usage
Non‑string field with a fixed message

```csharp
using UnityEngine;
using UnityEssentials;

public class HealthConfig : MonoBehaviour
{
    [Info("These values are clamped at runtime.", MessageType.Info)]
    public int MaxHealth = 100;
}
```

String field as a dynamic message (field hidden)

```csharp
public class BuildStatus : MonoBehaviour
{
    // When no message is provided in the attribute, the HelpBox shows this field’s value
    // and the text field itself is hidden.
    [Info]
    public string StatusMessage = "Warning: Local changes detected."; // prefix “Warning”/“Error” tweaks the icon
}
```

String field with a fixed message (field visible)

```csharp
public class NoteExample : MonoBehaviour
{
    // Shows a fixed Info box and also keeps the string field editable.
    [Info("You can keep notes here.", MessageType.Info, iconSize: 24)]
    public string Note;
}
```

Using message type and icon size

```csharp
[Info("Potential data loss!", MessageType.Error, iconSize: 40)]
public Object CriticalAsset;
```

## How It Works
- The drawer resolves the message in this order:
  1) If the attribute constructor provided a non‑empty message, it’s used
  2) Else if the field is a string, its current value is used as the message
- For string‑based messages, the drawer auto‑selects type by prefix:
  - Starts with “Error” → Error icon
  - Starts with “Warning” → Warning icon
  - Otherwise → falls back to the attribute’s `type` (default Info)
- Non‑string fields always render the default field after the HelpBox
- String fields render the default field only if the attribute explicitly provided a message; otherwise the field is hidden
- Box height is the max of measured text height and `iconSize`

## Notes and Limitations
- Supported field types: any serialized field; special handling for `string` as described
- The string‑bound mode hides the text field; to keep the text editable, provide a fixed message in the attribute
- Message type detection for string values is prefix‑based and case‑sensitive (“Warning”, “Error”)
- Editor‑only: this affects Inspector rendering only; it does not change runtime behavior

## Files in This Package
- `Runtime/InfoAttribute.cs` – `[Info]` attribute and `MessageType` enum
- `Editor/InfoDrawer.cs` – PropertyDrawer (HelpBox rendering, message resolution, icon size handling)
- `Runtime/UnityEssentials.InfoAttribute.asmdef` – Runtime assembly definition
- `Editor/UnityEssentials.InfoAttribute.Editor.asmdef` – Editor assembly definition

## Tags
unity, unity-editor, attribute, propertydrawer, helpbox, info, warning, error, inspector, ui, tools, workflow
