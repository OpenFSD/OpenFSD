#pragma once

namespace Server_Library
{
	class Praise0_Output
	{
	public:
		Praise0_Output();
		virtual ~Praise0_Output();

		__int8 Get_numberOfClientsConnected();
		void Set_numberOfClientsConnected(__int8 value);

	protected:

	private:
		static __int8 numberOfClientsConnected;
	};
}