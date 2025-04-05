#pragma once

namespace Server_Library
{
	class Praise0_Input
	{
	public:
		Praise0_Input();
		virtual ~Praise0_Input();

		int Get_Connection();
		void Set_Connection(int value);

	protected:

	private:
		static int connection;
	};
}