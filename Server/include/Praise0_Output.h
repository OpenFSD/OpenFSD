#pragma once
#include <vector>
namespace Server_Library
{
	class Praise0_Output
	{
	public:
		Praise0_Output();
		virtual ~Praise0_Output();

		bool Get_ping_Active();
		void Set_ping_Active(bool value);

	protected:

	private:
		static bool ping_Active;
	};
}