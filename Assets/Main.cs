//  
//     Copyright 2017 Abel Dantas
//  
//     This file is part of item-affix-decorators.
//     http://github.com/abeldantas/item-affix-decorators
//  
//     item-affix-decorators is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     item-affix-decorators is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program. If not, see http://www.gnu.org/licenses/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public Text Representation;
    public Text Title;
    public Text Effects;

    public void Awake()
    {
        Assert.IsNotNull( Representation );
    }

    List<Type> affixTypes = new List<Type>();

    public void Start()
    {
        affixTypes = ( from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
            from assemblyType in domainAssembly.GetTypes()
            where typeof(ItemAffix).IsAssignableFrom( assemblyType ) && !assemblyType.IsAbstract
            select assemblyType ).ToList();
    }

    public void OnCreateNewRandomItem()
    {
        // Create a wizard hat because that's the only item type we have right now
        var item = new WizardHat();
        Representation.text = item.Representation;

        // Get 2 random affixes
        var randomAffixes = affixTypes.GetRandomSample( Random.Range( 1, 4 ) );

        // Decorate it
        var a = randomAffixes.Aggregate<Type, Item>( item, ( b, c ) => (ItemAffix)Activator.CreateInstance( c, b ) );

        // Show the name and effect
        Title.text = a.Name;
        Effects.text = a.Description;
    }
}