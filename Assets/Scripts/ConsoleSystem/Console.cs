﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Console 
{
    private readonly IEnumerable<ConsoleCommand> commands;
    public List<string> commandHistory = new List<string>();
    public TurnManager turnManager;

    public Console(IEnumerable<ConsoleCommand> commands)
    {
        this.commands = commands;
    }
    public string ProcessInput(string inputValue)
    {
        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();

        return ProcessCommands(commandInput, args);
    }

    private string ProcessCommands(string commandInput, string[] args)
    {
        foreach (var command in commands)
        {
            if(!commandInput.Equals(command.CommandWord, System.StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            
            if(command.Process(args))
            {
                //eğer son elemanla aynı değilse kaydetme
                if( commandHistory.Count > 0 && !commandHistory.Last().Equals(commandInput + " " + string.Join(" ",args), System.StringComparison.OrdinalIgnoreCase))
                    commandHistory.Add( commandInput + " " + string.Join(" ",args));
                else if(commandHistory.Count == 0)
                    commandHistory.Add( commandInput + " " + string.Join(" ",args));
                
                turnManager.CallStartTurn();

                return "<#DEEF16><b>></b></color> <#00A2FF><b>" + commandInput + "</b></color> " + string.Join(" ", args);
            }
            else
            {
                return "<#DEEF16><b>></b></color> <#EF163D><b>" + commandInput + " " + string.Join(" ", args) + "</b></color> ";
            }
        }
        return "<#DEEF16><b>></b></color> <#EF163D><b>" + commandInput + " " + string.Join(" ", args) + "</b></color> ";
    }
    
}
