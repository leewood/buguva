<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
	  <xsl:template match="/">
    Your have:<br></br> 
    <xsl:apply-templates select="CardList/Card"/> 
  </xsl:template>
	<xsl:template match="Card">
	  <table>
	  <tr>
				<th>	
	   <h1>
	      <xsl:value-of select="attribute::id"></xsl:value-of>
	      .
	      <xsl:value-of select="Title"/>
	      
	   </h1> </th>
	   </tr>
	   <xsl:if test="@type='monster'">
	     <tr>
							<th>It is monster card so:</th>
		</tr>
	      <tr>
			<td>
			    Attack:
			</td>
			<td>
				Defence:			
			</td>
		  </tr>
		  <tr>
			<td>
			   <xsl:value-of select="AttackPoints"></xsl:value-of>
			</td>
			<td>
			   <xsl:value-of select="DefencePoints"></xsl:value-of>
			</td>
				
			</tr>
	   </xsl:if>
	   <xsl:apply-templates/>
	   </table>
	</xsl:template>
	<xsl:template match="Description">
	   <tr>
			<th>Description:
			   <xsl:value-of select="self::*"/>
			</th>
		</tr>
	      
	   
	</xsl:template>
	<xsl:template match="Visualization">
	   <tr>
		<td>Ways to see this card:
		
	   <ul>
					
		
	   <xsl:for-each select="View">
	       <xsl:sort data-type="text" select="@type" case-order="lower-first" order="ascending"/>
	       
	       <li>
			   <xsl:number format="1."></xsl:number>
			   <xsl:value-of select="@type">
			   </xsl:value-of>
			</li>
		
	   </xsl:for-each>
	   </ul>
	   </td>
	   </tr>
	</xsl:template>
	
	<xsl:template match="Events">
	   <tr>
			<th>Requied functions:</th>
		</tr>
		<tr>
			<th>
			<ul>
				<xsl:for-each select="Event/EventHandlingFunction">
				    <li>
				       <xsl:value-of select="ancestor::Event/@type">
				       </xsl:value-of>
				       :
				       <xsl:value-of select="@name"></xsl:value-of>(by default looks in: 
				       <xsl:value-of select="@src"></xsl:value-of>
				       )
				    </li>
				</xsl:for-each>
			</ul>
			
			</th>
		</tr>
	</xsl:template>
	<xsl:template match="StarsCount">
	    <tr>
			<th>It has stars:<xsl:value-of select="self::*"></xsl:value-of>
			</th>
			
		</tr>
	</xsl:template>
	<xsl:template match="Created">
	   <tr>
					<th>Card was created in <xsl:value-of select="self::*"></xsl:value-of></th>
		</tr>
	</xsl:template>
	<xsl:template match="Title">
	</xsl:template>
	<xsl:template match="AttackPoints">
	</xsl:template>
	<xsl:template match="DefencePoints">
	</xsl:template>
	
</xsl:stylesheet>
