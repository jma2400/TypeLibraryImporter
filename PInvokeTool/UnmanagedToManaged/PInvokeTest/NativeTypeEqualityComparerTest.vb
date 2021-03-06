﻿' Copyright (c) Microsoft Corporation.  All rights reserved.
'The following code was generated by Microsoft Visual Studio 2005.
'The test owner should check each test for validity.
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports PInvoke





'''<summary>
'''This is a test class for PInvoke.NativeTypeEqualityComparer and is intended
'''to contain all PInvoke.NativeTypeEqualityComparer Unit Tests
'''</summary>
<TestClass()> _
Public Class NativeTypeEqualityComparerTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

    '''<summary>
    ''' Basic test
    '''</summary>
    <TestMethod()> _
    Public Sub TopLevel1()
        Dim nt1 As New NativeBuiltinType(BuiltinType.NativeByte)
        Dim nt2 As New NativeBuiltinType(BuiltinType.NativeByte)

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(nt1, nt2))

        nt2 = New NativeBuiltinType(BuiltinType.NativeInt32)
        Assert.IsFalse(eq.Equals1(nt1, nt2))

    End Sub

    <TestMethod()> _
    Public Sub TopLevel3()
        Dim m1 As New NativeBitVector(5)
        Dim m2 As New NativeBitVector(5)

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(m1, m2))

        m2 = New NativeBitVector(6)
        Assert.IsFalse(eq.Equals1(m1, m2))
    End Sub

    ''' <summary>
    ''' Some proxy tests
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub TopLevel4()
        Dim m1 As New NativeBitVector(5)
        Dim m2 As New NativeBitVector(5)
        Dim p1 As New NativePointer(m1)
        Dim p2 As New NativePointer(m2)

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(p1, p2))

        p2.RealType = New NativeBitVector(6)
        Assert.IsFalse(eq.Equals1(p1, p2))
    End Sub

    <TestMethod()> _
    Public Sub TopLevel5()
        Dim m1 As New NativeBitVector(5)
        Dim m2 As New NativeBitVector(5)
        Dim p1 As New NativeArray(m1, 2)
        Dim p2 As New NativeArray(m2, 2)

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(p1, p2))

        p2.ElementCount = 3
        Assert.IsFalse(eq.Equals1(p1, p2))

        p2.RealType = New NativeBitVector(6)
        Assert.IsFalse(eq.Equals1(p1, p2))

    End Sub

    <TestMethod()> _
    Public Sub TopLevel6()
        Dim m1 As New NativeBitVector(5)
        Dim m2 As New NativeBitVector(5)
        Dim p1 As New NativeTypeDef("foo", m1)
        Dim p2 As New NativeTypeDef("foo", m2)

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(p1, p2))

        p2.Name = "bar"
        Assert.IsFalse(eq.Equals1(p1, p2))

        p2.Name = p1.Name
        p2.RealType = New NativeBitVector(6)
        Assert.IsFalse(eq.Equals1(p1, p2))
    End Sub

    <TestMethod()> _
    Public Sub TopLevel7()
        Dim m1 As New NativeBitVector(5)
        Dim m2 As New NativeBitVector(5)
        Dim p1 As New NativeNamedType("foo", m1)
        Dim p2 As New NativeNamedType("foo", m2)

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(p1, p2))

        ' We just dig right through named types unless they don't have an underlying
        ' real type
        p2.Name = "bar"
        Assert.IsTrue(eq.Equals1(p1, p2))

        p2.Name = p1.Name
        p2.RealType = New NativeBitVector(6)
        Assert.IsFalse(eq.Equals1(p1, p2))
    End Sub

    ''' <summary>
    ''' Struct comparison
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub TopLevel8()
        Dim nd1 As New NativeStruct("s")
        Dim nd2 As New NativeStruct("s")

        nd1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        nd2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(nd1, nd2))

        nd1.Members.Clear()
        Assert.IsFalse(eq.Equals1(nd1, nd2))

    End Sub

    ''' <summary>
    ''' Struct with sub structs that are different.  Not a problem for TopLevel
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub TopLevel12()
        Dim sub1 As New NativeStruct("sub")
        Dim sub2 As New NativeStruct("sub")
        sub1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        sub2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeChar)))

        Dim nd1 As New NativeStruct("n")
        Dim nd2 As New NativeStruct("n")

        nd1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        nd2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        nd1.Members.Add(New NativeMember("m2", sub1))
        nd2.Members.Add(New NativeMember("m2", sub2))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(nd1, nd2))
    End Sub

    ''' <summary>
    ''' When comparing members of a defined type at the top level, their type matches if you 
    ''' are comparing defined types and names
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub TopLevel13()
        Dim left As New NativeStruct("s")
        left.Members.Add(New NativeMember("m1", New NativeNamedType("foo")))

        Dim right As New NativeStruct("s")
        right.Members.Add(New NativeMember("m1", New NativeStruct("foo")))

        Assert.IsTrue(NativeTYpeEqualityComparer.AreEqualTopLevel(left, right))
    End Sub

    ''' <summary>
    ''' Recursively compare the structures
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub Recursive1()
        Dim sub1 As New NativeStruct("sub")
        Dim sub2 As New NativeStruct("sub")
        sub1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        sub2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        Dim nd1 As New NativeStruct("n")
        Dim nd2 As New NativeStruct("n")

        nd1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        nd2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        nd1.Members.Add(New NativeMember("m2", sub1))
        nd2.Members.Add(New NativeMember("m2", sub2))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.Recursive
        Assert.IsTrue(eq.Equals1(nd1, nd2))
    End Sub

    ''' <summary>
    ''' Sub structs differ so they are not recursively the same
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub Recursive2()
        Dim sub1 As New NativeStruct("sub")
        Dim sub2 As New NativeStruct("sub")
        sub1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        sub2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeChar)))

        Dim nd1 As New NativeStruct("n")
        Dim nd2 As New NativeStruct("n")

        nd1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        nd2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        nd1.Members.Add(New NativeMember("m2", sub1))
        nd2.Members.Add(New NativeMember("m2", sub2))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.Recursive
        Assert.IsFalse(eq.Equals1(nd1, nd2))
    End Sub

    ''' <summary>
    ''' Compare enumerations
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub EnumEquality1()
        Dim e1 As New NativeEnum("e")
        Dim e2 As New NativeEnum("e")

        e1.Values.Add(New NativeEnumValue("v1"))
        e2.Values.Add(New NativeEnumValue("v1"))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(e1, e2))
    End Sub

    ''' <summary>
    ''' Differing values mean they are not equal
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub EnumEquality2()
        Dim e1 As New NativeEnum("e")
        Dim e2 As New NativeEnum("e")

        e1.Values.Add(New NativeEnumValue("v1", "foo"))
        e2.Values.Add(New NativeEnumValue("v1"))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsFalse(eq.Equals1(e1, e2))
    End Sub

    ''' <summary>
    ''' Differing value names mean they are not equal
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub EnumEquality3()
        Dim e1 As New NativeEnum("e")
        Dim e2 As New NativeEnum("e")

        e1.Values.Add(New NativeEnumValue("v1"))
        e2.Values.Add(New NativeEnumValue("v2"))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsFalse(eq.Equals1(e1, e2))
    End Sub

    ''' <summary>
    ''' differing value order means they are not equal
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub EnumEquality4()
        Dim e1 As New NativeEnum("e")
        Dim e2 As New NativeEnum("e")

        e1.Values.Add(New NativeEnumValue("v2"))
        e1.Values.Add(New NativeEnumValue("v1"))

        e2.Values.Add(New NativeEnumValue("v1"))
        e2.Values.Add(New NativeEnumValue("v2"))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsFalse(eq.Equals1(e1, e2))
    End Sub

    ''' <summary>
    ''' Equality should just walk through named types
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub IgnoreNamedTypes()

        Dim e1 As New NativeEnum("e")
        Dim e2 As New NativeEnum("e")
        Dim n2 As New NativeNamedType("e", e2)

        e1.Values.Add(New NativeEnumValue("v1"))
        e2.Values.Add(New NativeEnumValue("v1"))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(e1, n2))
    End Sub

    ''' <summary>
    ''' Names should be ignored when comparing anonymous types
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub Anonymous1()
        Dim nd1 As New NativeStruct("ndaaoeu")
        Dim nd2 As New NativeStruct("a")
        nd1.IsAnonymous = True
        nd2.IsAnonymous = True

        nd1.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        nd2.Members.Add(New NativeMember("m1", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        nd1.Members.Add(New NativeMember("m2", New NativeBuiltinType(BuiltinType.NativeBoolean)))
        nd2.Members.Add(New NativeMember("m2", New NativeBuiltinType(BuiltinType.NativeBoolean)))

        Dim eq As NativeTypeEqualityComparer = NativeTypeEqualityComparer.TopLevel
        Assert.IsTrue(eq.Equals1(nd1, nd2))
    End Sub

End Class
