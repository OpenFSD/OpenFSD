#pragma once

namespace Server_Library
{
	class Praise0_Input
	{
	public:
		Praise0_Input();
		virtual ~Praise0_Input();

		bool Get_ping_Active();
		void Set_ping_Active(bool value);

	protected:

	private:
		static bool ping_Active;
	};
}