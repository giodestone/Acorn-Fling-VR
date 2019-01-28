using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionHelper
{
	//Get an average (mean) from more than two quaternions (with two, slerp would be used).
	//Note: this only works if all the quaternions are relatively close together.
	//Usage:
	//-Cumulative is an external Vector4 which holds all the added x y z and w components.
	//-newRotation is the next rotation to be added to the average pool
	//-firstRotation is the first quaternion of the array to be averaged
	//-addAmount holds the total amount of quaternions which are currently added
	//This function returns the current average quaternion
	public static Quaternion AverageQuaternion( ref Vector4 cumulative, Quaternion newRotation, float newWeight, Quaternion firstRotation, float totalAdded )
	{
		float w = 0.0f;
		float x = 0.0f;
		float y = 0.0f;
		float z = 0.0f;

		//Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
		//q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
		if( !AreQuaternionsClose( newRotation, firstRotation ) )
			newRotation = InverseSignQuaternion( newRotation );

		//Average the values
		float addDet = 1f / totalAdded;
		cumulative.w += newRotation.w * newWeight;
		w = cumulative.w * addDet;
		cumulative.x += newRotation.x * newWeight;
		x = cumulative.x * addDet;
		cumulative.y += newRotation.y * newWeight;
		y = cumulative.y * addDet;
		cumulative.z += newRotation.z * newWeight;
		z = cumulative.z * addDet;

		//note: if speed is an issue, you can skip the normalization step
		return NormalizeQuaternion( x, y, z, w );
	}

	public static Quaternion NormalizeQuaternion( float x, float y, float z, float w )
	{
		float lengthD = 1.0f / (w * w + x * x + y * y + z * z);

		w *= lengthD;
		x *= lengthD;
		y *= lengthD;
		z *= lengthD;

		return new Quaternion( x, y, z, w );
	}

	//Changes the sign of the quaternion components. This is not the same as the inverse.
	public static Quaternion InverseSignQuaternion( Quaternion q )
	{
		return new Quaternion( -q.x, -q.y, -q.z, -q.w );
	}

	//Returns true if the two input quaternions are close to each other. This can
	//be used to check whether or not one of two quaternions which are supposed to
	//be very similar but has its component signs reversed (q has the same rotation as
	//-q)
	public static bool AreQuaternionsClose( Quaternion q1, Quaternion q2 )
	{
		if( Quaternion.Dot( q1, q2 ) < 0.0f )
			return false;
		else
			return true;
	}

	public static Quaternion DeltaRotation( Quaternion rotation, Quaternion parentRotation )
	{
		return (rotation * Quaternion.Inverse( parentRotation ));           
	}

	public static Quaternion ApplyDelta( Quaternion deltaRot, Quaternion parentRot )
	{
		return (deltaRot * parentRot);
	}
}
