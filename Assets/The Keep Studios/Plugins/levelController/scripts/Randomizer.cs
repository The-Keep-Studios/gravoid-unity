using System;
using System.Collections.Generic;
//using System.Text;

public static class Randomizer{

	#region FisherYatesShuffle

    /// <summary>
    /// returns a random draw of entries of sourceList of random length. 
    /// Any specific entry in sourceList can at most be used once in 
    /// the resulting random array. 
    /// </summary>
    /// <param name="sourceList">a list containing all the possible elements.</param>
    /// <param name="random">a random number generator used for generating random indexies and a random size.</param>
    /// <returns>random draw of random length</returns>
    public static List<T> GetRandomListSubset<T>(List<T> sourceList, Random random)
    {
        int size = random.Next(sourceList.Count);
        return GetRandomList<T>(sourceList, size, random);
    }
	
	
    public static List<T> GetRandomList<T>( List<T> sourceList ){
		
		return GetRandomList<T>(sourceList
				, sourceList.Count
				, new System.Random ());
		
	}

    /// <summary>
    /// returns a random draw of entries of sourceList of static length. 
    /// Any specific entry in sourceList can at most be used once in 
    /// the resulting random array. The returned array is construced 
    /// using the Fisher-Yates shuffle algorithm.
    /// http://www.nist.gov/dads/HTML/fisherYatesShuffle.html
    /// </summary>
    /// <param name="sourceList">source of elements to randomize.</param>
    /// <param name="size">size of array to return.</param>
    /// <param name="randomNumGenerator">number generator to use for randomization.</param>
    /// <returns>random draw of length size</returns>
    public static List<T> GetRandomList<T>(List<T> sourceList, int size, Random randomNumGenerator){
		
        if (size > sourceList.Count){
			
            throw new ArgumentException("Size can't be larger than count of elements in SourceList", "size");
			
        }
		else if (size < 0){
			
            throw new ArgumentException("Size can't be negative", "size");
			
        }

        List<T> randomList = new List<T>(sourceList);
		
		//randomize the first n items where n is equal to size.
		for (int nextIdxToRandomize = 0; nextIdxToRandomize < size - 1; nextIdxToRandomize++){
			
            int swapPosition = nextIdxToRandomize + randomNumGenerator.Next(size - nextIdxToRandomize);
			
            T swap = randomList[swapPosition];
			
            randomList[swapPosition] = randomList[nextIdxToRandomize];
			
            randomList[nextIdxToRandomize] = swap;
            
        }
		
		if( size < randomList.Count ){
			
			randomList.RemoveRange(size, randomList.Count-1);
			
		}
		
        return randomList;
    }

    #endregion
}
