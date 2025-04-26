#pragma once

namespace Server_Library
{
	class Praise2_Input
	{
	public:
		Praise2_Input();
		virtual ~Praise2_Input();

		bool Get_fowards();
		bool Get_backwards();
		bool Get_left();
		bool Get_right();
		float Get_period();

		void Set_fowards(bool value);
		void Set_backwards(bool value);
		void Set_left(bool value);
		void Set_right(bool value);
		void Set_period(float value);

	protected:

	private:
		static bool _fowards;
		static bool _backwards;
		static bool _left;
		static bool _right;
		static float period;
	};
}