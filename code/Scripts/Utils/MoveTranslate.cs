using System;

public sealed class MoveTranslate : Component {
	[Property] public float Speed { get; set; }
  [Property] public Vector3 Step { get; set; }
  [Property] public Vector3 Length { get; set; }

	protected override void OnFixedUpdate()
	{
		Translate();
	}

	private void Translate(){
    Vector3 moveTo = LocalPosition.LerpTo(new Vector3(
      Step.x == 0 ? 0 : MathF.Sin(Time.Now * Speed ) * Step.x * Length.x,
      Step.y == 0 ? 0 : MathF.Sin(Time.Now * Speed ) * Step.y * Length.y,
      0
    ), Time.Delta);
    LocalPosition = moveTo;
  }
}