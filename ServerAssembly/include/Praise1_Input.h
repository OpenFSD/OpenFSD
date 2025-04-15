#pragma once

namespace Server_Library
{
	class Praise1_Input
	{
	public:
		Praise1_Input();
		virtual ~Praise1_Input();

		float Get_mouse_X();
		float Get_mouse_Y();

		void Set_mouse_X(float value);
		void Set_mouse_Y(float value);

	protected:

	private:
		static float mouse_X;
		static float mouse_Y;
	};
}