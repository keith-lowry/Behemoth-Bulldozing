using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    /**
     * Sets up the testing environment. Called BEFORE
     * each test method.
     */
    [SetUp]
    public void SetUp()
    {

    }

    /**
     * Resets the testing environment. Called AFTER
     * each test method.
     *
     * Destroy any objects/artifacts between unit tests
     * in this method.
     */
    [TearDown]
    public void TearDown()
    {

    }

    // A Test behaves as an ordinary method
    [Test] //NEED ATTRIBUTE
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest] //NEED ATTRIBUTE
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null; //NEED YIELD FOR COROUTINE (IENUMERATOR)
    }
}
