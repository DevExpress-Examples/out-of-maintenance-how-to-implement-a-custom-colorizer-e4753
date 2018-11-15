<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/CustomColorizer/Form1.cs) (VB: [Form1.vb](./VB/CustomColorizer/Form1.vb))
<!-- default file list end -->
# How to implement a custom colorizer


<p>This example demonstrates how to create a custom colorizer and use it for implementing a progress bar on a map.</p><p>The colorizer fills bars with colors according to the specified power value stored as a shape attribute.</p>


<h3>Description</h3>

<p>To implement a custom colorizer, create a class inherited from the <strong>MapColorizer</strong> base class and implement the <strong>ColorizeElement</strong> method according to your custom rules.</p>
<p>For example, in this case, the custom colorizer does the following:</p>
<p>1. Obtains the power value from the shape attribute (via the <strong>MapItem.Attributes</strong> property)<br /> 2. Chooses a color that corresponds to this value (e.g. from the predefined array of <strong>Color</strong> objects) <br /> 3. And assigns this color to the <strong>IColorizerElement.ColorizerColor</strong> property of the element passed to the <strong>ColorizeElement</strong> method.</p>

<br/>


