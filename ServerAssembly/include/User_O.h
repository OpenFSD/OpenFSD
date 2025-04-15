#pragma once
#include "Praise0_Output.h"
#include "Praise1_Output.h"
#include "Praise2_Output.h"

namespace Server_Library
{
	class User_O
	{
	public:
		User_O();
		virtual ~User_O();
		class Praise0_Output* Get_Praise0_Output();
		class Praise1_Output* Get_Praise1_Output();
		class Praise2_Output* Get_Praise2_Output();

	protected:

	private:
		static class Praise0_Output* ptr_Praise0_Output;
		static class Praise1_Output* ptr_Praise1_Output;
		static class Praise2_Output* ptr_Praise2_Output;
	};
}