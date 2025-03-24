using Skyline.DataMiner.Analytics.GenericInterface;

[GQIMetaData(Name = "Negative number transformer")]
public class NegativeNumberTransformer : IGQIRowOperator, IGQIInputArguments
{
	private static readonly GQIColumnDropdownArgument _sourceArg = new GQIColumnDropdownArgument("Source") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.Double } };
	private static readonly GQIDoubleArgument _newvalueArg = new GQIDoubleArgument("New value") { IsRequired = true };

	private GQIColumn<double> _sourceColumn;
	private double _newValue;

	public GQIArgument[] GetInputArguments()
	{
		return new GQIArgument[] { _sourceArg, _newvalueArg };
	}

	public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
	{
		_sourceColumn = args.GetArgumentValue(_sourceArg) as GQIColumn<double>;
		_newValue = args.GetArgumentValue(_newvalueArg);

		return new OnArgumentsProcessedOutputArgs();
	}

	public void HandleRow(GQIEditableRow row)
	{
		double oldValue = row.GetValue<double>(_sourceColumn);
		if (oldValue < 0)
			row.SetValue(_sourceColumn, _newValue);
	}
}