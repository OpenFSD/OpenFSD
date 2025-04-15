#pragma once
#include <vector>

namespace Server_Library
{
	class Praise2_Output
	{
	public:
		Praise2_Output();
		virtual ~Praise2_Output();
		static std::vector<float> Get_position();
		static std::vector<float> Get_front();
		static std::vector<float> Get_up();
		static std::vector<float> Get_right();

		static void Set_position(std::vector<float> value);
		static void Set_front(std::vector<float> value);
		static void Set_up(std::vector<float> value);
		static void Set_right(std::vector<float> value);

	protected:

	private:
		static std::vector<float> _position;
		static std::vector<float> _front;
		static std::vector<float> _up;
		static std::vector<float> _right;
	};
}